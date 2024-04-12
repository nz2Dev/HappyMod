using System;
using System.Collections;
using UnityEngine;

public class RootInteractor {

    private RootRouter router;
    private MonoBehaviourService monoService;

    private bool noInternetMessageDisplayed;

    public void SetComponents(RootRouter router, MonoBehaviourService monoService) {
        this.router = router;
        this.monoService = monoService;
    }

    public void Activate() {
        monoService.StartCoroutine(InternetConnectionListener());
    }

    private IEnumerator InternetConnectionListener() {
        while (true) {
            if (!noInternetMessageDisplayed) {
                if (!IsNetworkReachable()) {
                    router.ShowAlert("Internet connection lost", "Check your internet connection and come back to us.", "Ok", onClosed: () => {
                        noInternetMessageDisplayed = true;
                    });
                }
            } else {
                if (IsNetworkReachable()) {
                    noInternetMessageDisplayed = false;
                }
            }

            yield return new WaitForSecondsRealtime(0.5f);
        }
    }

    private bool IsNetworkReachable() {
        return Application.internetReachability != NetworkReachability.NotReachable;
    }
}