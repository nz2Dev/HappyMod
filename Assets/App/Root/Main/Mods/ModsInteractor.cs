using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Plugins.Dropbox;
using UnityEngine;

public class ModsInteractor {

    private readonly ModRepository modRepository;

    private Dictionary<string, ModItem> modDictionary = new();
    private string filterString = "";
    private string filterCategory;
    private bool filterIsDirty;

    public ModsInteractor(ModRepository modRepository) {
        this.modRepository = modRepository;
    }

    private ModsUI ui;
    private ModsRouter router;
    private MonoBehaviourService monoService;

    public void SetComponents(ModsUI ui, ModsRouter router, MonoBehaviourService monoService) {
        this.ui = ui;
        this.router = router;
        this.monoService = monoService;

        ui.OnItemDownloadButtonClicked += UIOnItemDonwloadButtonClicked;
        ui.OnSearchBarInputChanged += UIOnSearchBarInputChanged;
        ui.OnCategoryBarSelectionChanged += UIOnCategoryBarSelectionChanged;
    }

    private void UIOnCategoryBarSelectionChanged(string newSelectedCategory) {
        filterCategory = newSelectedCategory;
        filterIsDirty = true;
    }

    private void UIOnSearchBarInputChanged(string newInput) {
        filterString = newInput;
        filterIsDirty = true;
    }

    private void UIOnItemDonwloadButtonClicked(string itemKey) {
        monoService.StartCoroutine(DownloadModFile(itemKey));
    }

    public void Activate() {
        monoService.StartCoroutine(LoadConfigs());
        monoService.StartCoroutine(FilterChangesListener());
    }

    private IEnumerator FilterChangesListener() {
        while (true) {
            yield return new WaitForSecondsRealtime(0.2f);
            if (filterIsDirty) {
                filterIsDirty = false;

                var filteredItems = modDictionary.Values
                    .Where(modItem => modItem.Data.category.Equals(filterCategory))
                    .Where(modItem => modItem.Data.title.Contains(filterString, StringComparison.OrdinalIgnoreCase))
                    .ToArray();
                
                ui.SetModItems(filteredItems);
                ui.SetNoResultMessageDisplay(filteredItems.Length == 0);
            }
        }
    }

    private IEnumerator DownloadModFile(string itemKey) {
        var modItem = modDictionary[itemKey];
        modItem.DownloadingProgress = true;
        ui.UpdateModItem(modItem);

        var fileRelativePath = modItem.Data.filePath[1..];
        var downloadTask = DropboxHelper.DownloadAndSaveFile(fileRelativePath);   
        yield return new WaitUntil(() => downloadTask.IsCompleted);
        modItem.DownloadingProgress = false;
        ui.UpdateModItem(modItem);

        if (downloadTask.IsCompletedSuccessfully) {
            var filePath = DropboxHelper.GetDownloadedFilePathInPersistentStorage(fileRelativePath);
            new NativeShare().AddFile(filePath).Share();
        }
    }

    private IEnumerator LoadConfigs() {
        var modsTask = modRepository.GetModsConfig();
        yield return new WaitUntil(() => modsTask.IsCompleted);

        if (modsTask.IsCompletedSuccessfully) {
            ui.SetCategories(modsTask.Result.categories);

            modDictionary = modsTask.Result.mods.Select(mod => new ModItem(mod)).ToDictionary(item => item.Key);
            var allItemsArray = modDictionary.Values.ToArray();
            
            ui.SetModItems(allItemsArray);
            ui.SetNoResultMessageDisplay(allItemsArray.Length == 0);
            
            yield return LoadCachedPreviews(allItemsArray);
            yield return ReDownloadPreviews(allItemsArray);
        }
    }

    private IEnumerator LoadCachedPreviews(ModItem[] itemaArray) {
        foreach (var modItem in itemaArray) {
            var imageRelativePath = modItem.Data.previewPath[1..];
            var imageFilePath = DropboxHelper.GetDownloadedFilePathInPersistentStorage(imageRelativePath);
            
            if (File.Exists(imageFilePath)) {
                var cacheReadingTask = File.ReadAllBytesAsync(imageFilePath);
                yield return new WaitUntil(() => cacheReadingTask.IsCompleted);    

                var cachedTexture = new Texture2D(2, 2);
                cachedTexture.LoadImage(cacheReadingTask.Result);
                modItem.PreviewImage = cachedTexture;
                ui.UpdateModItem(modItem);
            }
        }
    }

    private IEnumerator ReDownloadPreviews(ModItem[] itemsArray) {
        foreach (var modItem in itemsArray) {
            var imageRelativePath = modItem.Data.previewPath[1..];

            var downloadTask = DropboxHelper.DownloadAndSaveFile(imageRelativePath); 
            yield return new WaitUntil(() => downloadTask.IsCompleted);

            var imageFilePath = DropboxHelper.GetDownloadedFilePathInPersistentStorage(imageRelativePath);
            var readingTask = File.ReadAllBytesAsync(imageFilePath);
            yield return new WaitUntil(() => readingTask.IsCompleted);

            var texture = modItem.PreviewImage;
            if (texture == null) {
                texture = new Texture2D(2, 2);
            }

            texture.LoadImage(readingTask.Result);
            modItem.PreviewImage = texture;
            ui.UpdateModItem(modItem);
        }
    }
    
}