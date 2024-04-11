using UnityEngine;

public class ModsRouter {

    private readonly RectTransform placementSlot;
    private readonly ModsUI ui;
    private readonly ModsInteractor interactor;

    public ModsRouter(RectTransform placementSlot, ModsUI ui, ModsInteractor interactor) {
        this.placementSlot = placementSlot;
        this.ui = ui;
        this.interactor = interactor;
    }

    public void OnAttached() {
        ui.SpawnUIElements(placementSlot);
        interactor.SetComponents(ui, this);
        interactor.Activate();
    }

    public void OnDetached() {
        ui.DeleteUIElements();
    }

}