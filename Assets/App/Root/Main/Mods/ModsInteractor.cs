using System;
using System.Collections;
using System.IO;
using System.Linq;
using Plugins.Dropbox;
using UnityEngine;
using UnityEngine.Networking;

public class ModsInteractor {

    private readonly ModRepository modRepository;

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
    }

    public void Activate() {
        monoService.StartCoroutine(LoadConfigs());
    }

    private IEnumerator LoadConfigs() {
        var modsTask = modRepository.GetModsConfig();
        yield return new WaitUntil(() => modsTask.IsCompleted);

        if (modsTask.IsCompletedSuccessfully) {
            ui.SetCategories(modsTask.Result.categories);
            ui.SetMods(modsTask.Result.mods);

            foreach (var mod in modsTask.Result.mods) {
                var path = mod.previewPath[1..];
                var downloadTask = DropboxHelper.DownloadAndSaveFile(path); 
                yield return new WaitUntil(() => downloadTask.IsCompleted);

                var imagePath = DropboxHelper.GetDownloadedFilePathInPersistentStorage(path);
                var readingTask = File.ReadAllBytesAsync(imagePath);
                yield return new WaitUntil(() => readingTask.IsCompleted);

                var texture = new Texture2D(2, 2);
                texture.LoadImage(readingTask.Result);
                
                ui.SetModItemTexture(mod, texture);
            }
        }
    }
    
}