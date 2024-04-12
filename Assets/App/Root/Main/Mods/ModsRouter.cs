using System;
using UnityEngine;

public class ModsRouter {

    private readonly RectTransform placementSlot;
    private readonly ModsUI ui;
    private readonly ModsInteractor interactor;

    private MonoBehaviourService monoService;

    public ModsRouter(RectTransform placementSlot, ModsUI ui, ModsInteractor interactor) {
        this.placementSlot = placementSlot;
        this.ui = ui;
        this.interactor = interactor;
    }

    public void OnAttached() {
        monoService = new GameObject().AddComponent<MonoBehaviourService>();
        ui.SpawnUIElements(placementSlot);
        interactor.SetComponents(ui, this, monoService);
        interactor.Activate();
    }

    public void OnResumed() {
        ui.Show();
    }

    public void OnPaused() {
        ui.Hide();
    }

    public void OnDetached() {
        GameObject.Destroy(monoService.gameObject);
        ui.DeleteUIElements();
    }

}