using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class ModsInteractor {
    private ModsUI ui;
    private ModsRouter router;

    public void SetComponents(ModsUI ui, ModsRouter router) {
        this.ui = ui;
        this.router = router;
    }

    public void Activate() {
        var monoService = new GameObject().AddComponent<MonoBehaviourService>();
        monoService.StartCoroutine(LoadConfigs());
    }

    private IEnumerator LoadConfigs() {
        var modRepository = new ModRepository();
        var modsTask = modRepository.GetModsConfig();
        yield return new WaitUntil(() => modsTask.IsCompleted);

        foreach (var category in modsTask.Result.categories) {
            Debug.Log("read category: " + category);
        }
        
        foreach (var mod in modsTask.Result.mods) {
            Debug.Log("read mod: " + mod.title);
        }
    }
    
}