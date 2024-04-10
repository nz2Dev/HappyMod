using System;

public class MainInteractor {

    private MainUI ui;
    private MainRouter router;

    public void SetComponents(MainUI ui, MainRouter router) {
        this.ui = ui;
        this.router = router;    

        ui.OnTabClicked += UIOnTabClicked;
    }

    public void Activate() {
        router.AttachMods();
        ui.SetTabSelected(MainUI.Tab.Mods);
    }

    private void UIOnTabClicked(MainUI.Tab tab) {
        if (tab == MainUI.Tab.Mods) {
            router.AttachMods();   
        } else {
            router.AttachPlaceholder();
        }
    }

}