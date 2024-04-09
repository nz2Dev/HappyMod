using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scopes/Loading")]
public class LoadingScope : ScriptableObject {

    [SerializeField] private RectTransform elementsPrefab;
    
    public LoadingRouter Router(RectTransform placementSlot) {
        return new LoadingRouter(
            placementSlot,
            new LoadingUI(elementsPrefab),
            new LoadingInteractor(),
            this
        );
    }

}
