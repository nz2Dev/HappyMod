using JetBrains.Annotations;
using UnityEditor.SceneTemplate;
using UnityEngine;

public class LoadingUI {
    
    private readonly LoadingTree treePrefab;
    
    private LoadingTree tree;

    public LoadingUI(LoadingTree treePrefab) {
        this.treePrefab = treePrefab;
    }

    public void SpawnUIElements(RectTransform placementSlot) {
        tree = Object.Instantiate(treePrefab.gameObject, placementSlot).GetComponent<LoadingTree>();
    }

    public void SetLoadingText() {
        tree.title.text = "Loading text!";
    }

}