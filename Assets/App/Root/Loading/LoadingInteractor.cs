using System;
using System.Collections;
using Plugins.Dropbox;
using UnityEngine;

public class LoadingInteractor {

    private LoadingUI ui;
    private LoadingRouter router;
    private MonoBehaviourService monoService;

    public void SetComponents(LoadingUI ui, LoadingRouter router, MonoBehaviourService monoService) {
        this.ui = ui;
        this.router = router;
        this.monoService = monoService;
    }

    public void Activate() {
        monoService.StartCoroutine(LoadConfigs());
    }

    private IEnumerator LoadConfigs() {
        ui.SetLoadingProgress(0);

        var initTask = DropboxHelper.Initialize();
        yield return new WaitUntil(() => initTask.IsCompleted);
        if (initTask.IsCompletedSuccessfully) {
            ui.SetLoadingProgress(0.1f);

            var savingTask = DropboxHelper.DownloadAndSaveFile("mods.json");            
            yield return new WaitForSeconds(0.1f);
            while (!savingTask.IsCompleted) {
                yield return new WaitForSeconds(0.1f);
            }

            if (savingTask.IsCompletedSuccessfully) {
                ui.SetLoadingProgress(1f);
                router.DispatchOnLoaded();
            } else {
                ui.SetLoadingProgress(0f);
            }
        }
    }

}