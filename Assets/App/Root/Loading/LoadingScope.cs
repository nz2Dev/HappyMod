using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scopes/Loading")]
public class LoadingScope : ScriptableObject {

    [SerializeField] private LoadingTree treePrefab;
    [SerializeField] private MonoBehaviourService servicePrefab;

    public LoadingRouter Router(RectTransform placementSlot) {
        return new LoadingRouter(
            placementSlot,
            servicePrefab,
            new LoadingUI(treePrefab),
            new LoadingInteractor(new ModRepository()),
            this
        );
    }

}
