using JetBrains.Annotations;
using UnityEditor.SceneTemplate;
using UnityEngine;

public class LoadingUI {
    
    private readonly RectTransform elementsPrefab;
    
    private RectTransform rootTransform;

    public LoadingUI(RectTransform elementsPrefab) {
        this.elementsPrefab = elementsPrefab;
    }

    public void SpawnUIElements(RectTransform placementSlot) {
        rootTransform = (RectTransform) GameObject.Instantiate(elementsPrefab.gameObject, placementSlot).transform;
    }

}