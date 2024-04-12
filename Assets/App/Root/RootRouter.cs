using System;
using UnityEngine;

public class RootRouter {

    private readonly Canvas placementCanvas;
    private readonly AlertControl alertControlPrefab;
    private readonly RootInteractor interactor;
    private readonly RootScope scope;

    private LoadingRouter loadingRouter;
    private MainRouter mainRouter;

    public RootRouter(Canvas canvas, AlertControl alertControlPrefab, RootInteractor interactor, RootScope scope) {
        this.placementCanvas = canvas;
        this.alertControlPrefab = alertControlPrefab;
        this.interactor = interactor;
        this.scope = scope;
    }

    public void OnAttached() {
        var monoService = new GameObject().AddComponent<MonoBehaviourService>();
        interactor.SetComponents(this, monoService);
        interactor.Activate();

        loadingRouter = scope.LoadingScope().Router((RectTransform) placementCanvas.transform);
        loadingRouter.OnLoaded += LoadingRouterOnLoaded;
        loadingRouter.OnAttached();
    }

    private void LoadingRouterOnLoaded() {
        loadingRouter.OnDetached();
        loadingRouter = null;

        mainRouter = scope.MainScope().Router((RectTransform) placementCanvas.transform);
        mainRouter.OnAttached();
    }

    private AlertControl currentAlert;

    public void ShowAlert(string title = null, string message = null, string button = null, Action onClosed = null) {
        HideAlertInternal();

        currentAlert = GameObject.Instantiate(alertControlPrefab.gameObject, placementCanvas.transform).GetComponent<AlertControl>();
        currentAlert.SetTitle(title);
        currentAlert.SetMessage(message);
        currentAlert.SetButton(button);
        currentAlert.OnButtonClick += () => {
            onClosed?.Invoke();
            HideAlertInternal();
        };
        currentAlert.OnOutsideBoxClick += () => {
            onClosed?.Invoke();
            HideAlertInternal();
        };
    }

    private void HideAlertInternal() {
        if (currentAlert != null) {
            GameObject.Destroy(currentAlert.gameObject);
            currentAlert = null;
        }
    }
    
}