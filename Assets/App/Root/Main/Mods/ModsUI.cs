using UnityEngine;

public class ModsUI {
    
    private readonly ModsTree modsTreePrefab;

    private ModsTree tree;

    public ModsUI(ModsTree modsTreePrefab) {
        this.modsTreePrefab = modsTreePrefab;
    }

    public void SpawnUIElements(RectTransform placementSlot) {
        tree = GameObject.Instantiate(modsTreePrefab.gameObject, placementSlot).GetComponent<ModsTree>();
    }

    public void DeleteUIElements() {
        GameObject.Destroy(tree);
    }

    public void SetCategories(string[] categories) {
        tree.categoryBar.SetCategories(categories);
    }
    
}