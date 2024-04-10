using UnityEngine;

public class LoadingRouter {

    private readonly RectTransform placementSlot;
    private readonly MonoBehaviourService monoServicePrefab;
    private readonly LoadingUI ui;
    private readonly LoadingInteractor interactor;
    private readonly LoadingScope scope;

    private MonoBehaviourService monoService;

    public LoadingRouter(RectTransform placementSlot, MonoBehaviourService monoServicePrefab, LoadingUI ui, LoadingInteractor interactor, LoadingScope scope) {
        this.placementSlot = placementSlot;
        this.monoServicePrefab = monoServicePrefab;
        this.ui = ui;
        this.interactor = interactor;
        this.scope = scope;
    }

    public void OnAttached() {
        monoService = Object.Instantiate(monoServicePrefab.gameObject).GetComponent<MonoBehaviourService>();
        ui.SpawnUIElements(placementSlot);
        interactor.SetComponents(ui, this, monoService);
        interactor.Activate();
    }

    public void OnDetached() {
        Object.Destroy(monoService);
        ui.DeleteUIElements();
    }

}