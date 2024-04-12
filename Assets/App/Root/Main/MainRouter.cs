using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainRouter {

    private readonly RectTransform placementSlot;
    private readonly MainUI ui;
    private readonly MainInteractor interactor;
    private readonly MainScope scope;

    private ModsRouter modsRouter;
    private GameObject placeholderTree;

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

    public void AttachMods() {
        if (placeholderTree != null) {
            Object.Destroy(placeholderTree);
        }

        if (modsRouter == null) {
            modsRouter = scope.ModsScope().Router(ui.ContentContainer);
            modsRouter.OnAttached();
        }

        modsRouter.OnResumed();
    }

    public void AttachPlaceholder() {
        if (modsRouter != null) {
            modsRouter.OnPaused();
        }

        if (placeholderTree == null) {
            placeholderTree = GameObject.Instantiate(scope.PlaceholderTreePrfab(), ui.ContentContainer);
        }
    }
    
}
