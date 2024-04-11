using System;
using System.Collections;
using Plugins.Dropbox;
using UnityEngine;

public class LoadingInteractor {

    private readonly ModRepository modRepository;

    public LoadingInteractor(ModRepository modRepository) {
        this.modRepository = modRepository;
    }

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
        ui.SetLoadingProgress(0.1f);

        var downloadingTask = modRepository.DownloadModsConfig();
        yield return new WaitUntil(() => downloadingTask.IsCompleted);

        if (downloadingTask.IsCompletedSuccessfully) {
            ui.SetLoadingProgress(1f);
            router.DispatchOnLoaded();
        } else {
            ui.SetLoadingProgress(0f);
        }
    }

}