using UnityEngine;

public class ModsRouter {

    private readonly RectTransform placementSlot;
    private readonly GameObject treePrefab;

    public ModsRouter(RectTransform placementSlot, GameObject treePrefab) {
        this.placementSlot = placementSlot;
        this.treePrefab = treePrefab;
    }

    private GameObject tree;

    public void OnAttached() {
        tree = Object.Instantiate(treePrefab, placementSlot);
    }

    public void OnDetached() {
        Object.Destroy(tree);
    }
}