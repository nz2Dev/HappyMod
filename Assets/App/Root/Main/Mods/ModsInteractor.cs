using System;

public class ModsInteractor {
    private ModsUI ui;
    private ModsRouter router;

    public void SetComponents(ModsUI ui, ModsRouter router) {
        this.ui = ui;
        this.router = router;
    }

    public void Activate() {
        throw new NotImplementedException();
    }
    
}