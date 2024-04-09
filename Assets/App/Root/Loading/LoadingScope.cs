using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scopes/Loading")]
public class LoadingScope : ScriptableObject {

    [SerializeField] private LoadingTree treePrefab;
    
    public LoadingRouter Router(RectTransform placementSlot) {
        return new LoadingRouter(
            placementSlot,
            new LoadingUI(treePrefab),
            new LoadingInteractor(),
            this
        );
    }

}
