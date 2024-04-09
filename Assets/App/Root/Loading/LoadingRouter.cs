using System;
using UnityEngine;

public class LoadingRouter {

    private readonly RectTransform placementSlot;
    private readonly LoadingUI ui;
    private readonly LoadingInteractor interactor;
    private readonly LoadingScope scope;

    public LoadingRouter(RectTransform placementSlot, LoadingUI ui, LoadingInteractor interactor, LoadingScope scope) {
        this.placementSlot = placementSlot;
        this.ui = ui;
        this.interactor = interactor;
        this.scope = scope;
    }

    public void OnAttached() {
        ui.SpawnUIElements(placementSlot);
        interactor.SetComponents(ui, this);
        interactor.Activate();
    }

}