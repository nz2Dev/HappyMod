using System;
using System.Collections;
using System.Linq;
using UnityEngine;

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
        }
    }
    
}