using JetBrains.Annotations;
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

    public void SetLoadingProgress(float progress) {
        tree.progressBarImage.fillAmount = progress;
    }

    public void DeleteUIElements() {
        Object.Destroy(tree.gameObject);
    }
    
}