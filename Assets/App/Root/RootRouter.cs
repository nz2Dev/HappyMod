using System;
using UnityEngine;

public class RootRouter {

    private readonly Canvas placementCanvas;
    private readonly RootScope scope;

    private LoadingRouter loadingRouter;

    public RootRouter(Canvas canvas, RootScope scope) {
        this.placementCanvas = canvas;
        this.scope = scope;
    }

    public void OnAttached() {
        loadingRouter = scope.LoadingScope().Router((RectTransform) placementCanvas.transform);
        loadingRouter.OnLoaded += LoadingRouterOnLoaded;
        loadingRouter.OnAttached();
    }

    private void LoadingRouterOnLoaded() {
        loadingRouter.OnDetached();
        loadingRouter = null;
    }
    
}