using UnityEngine;

public class ModsRouter {

    private readonly RectTransform placementSlot;
    private readonly ModsUI ui;

    public ModsRouter(RectTransform placementSlot, ModsUI ui) {
        this.placementSlot = placementSlot;
        this.ui = ui;
    }

    public void OnAttached() {
        ui.SpawnUIElements(placementSlot);
    }

    public void OnDetached() {
        ui.DeleteUIElements();
    }
    
}