using System;

public class MainInteractor {

    private MainUI ui;
    private MainRouter router;

    public void SetComponents(MainUI ui, MainRouter router) {
        this.ui = ui;
        this.router = router;    
    }

    public void Activate() {
        throw new NotImplementedException();
    }
}