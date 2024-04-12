using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Plugins.Dropbox;
using UnityEngine;
using UnityEngine.Networking;

public class ModsInteractor {

    private readonly ModRepository modRepository;

    private Dictionary<string, ModItem> modDictionary = new();
    private string filterString = "";
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
                    .Where(modItem => modItem.Data.title.Contains(filterString, StringComparison.OrdinalIgnoreCase));

                ui.SetModItems(filteredItems);
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
            ui.SetModItems(modDictionary.Values);

            foreach (var modItem in modDictionary.Values.ToArray()) {
                var imageRelativePath = modItem.Data.previewPath[1..];
                var downloadTask = DropboxHelper.DownloadAndSaveFile(imageRelativePath); 
                yield return new WaitUntil(() => downloadTask.IsCompleted);

                var imagePath = DropboxHelper.GetDownloadedFilePathInPersistentStorage(imageRelativePath);
                var readingTask = File.ReadAllBytesAsync(imagePath);
                yield return new WaitUntil(() => readingTask.IsCompleted);

                var texture = new Texture2D(2, 2);
                texture.LoadImage(readingTask.Result);
                modItem.PreviewImage = texture;
                
                ui.UpdateModItem(modItem);
            }
        }
    }
    
}