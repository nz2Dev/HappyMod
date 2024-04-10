using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainRouter {

    private readonly RectTransform placementSlot;
    private readonly MainUI ui;
    private readonly MainInteractor interactor;
    private readonly MainScope scope;

    public MainRouter(RectTransform placementSlot, MainUI ui, MainInteractor interactor, MainScope scope) {
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
