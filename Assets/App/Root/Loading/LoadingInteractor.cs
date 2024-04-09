using System;

public class LoadingInteractor {

    private LoadingUI ui;
    private LoadingRouter router;

    public void SetComponents(LoadingUI ui, LoadingRouter router) {
        this.ui = ui;
        this.router = router;
    }

    public void Activate() {
        ui.SetLoadingText();
    }

}