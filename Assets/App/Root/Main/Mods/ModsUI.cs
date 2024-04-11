using System;
using System.Linq;
using UnityEngine;

public class ModsUI {
    
    private readonly ModsTree modsTreePrefab;
    private readonly ModItemTree modItemTreePrefab;

    private ModsTree tree;

    public ModsUI(ModsTree modsTreePrefab, ModItemTree modItemTreePrefab) {
        this.modsTreePrefab = modsTreePrefab;
        this.modItemTreePrefab = modItemTreePrefab;
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

    public void SetMods(Mod[] mods) {
        var rowsChilds = tree.modsRows.transform.Cast<Transform>().ToArray();
        foreach (var rowChild in rowsChilds) {
            GameObject.Destroy(rowChild.gameObject);
        }

        foreach (var mod in mods) {
            var modItemTree = GameObject.Instantiate(modItemTreePrefab.gameObject, tree.modsRows.transform).GetComponent<ModItemTree>();
            modItemTree.SetKey(mod.previewPath);
            modItemTree.SetTitle(mod.title);
            modItemTree.SetDescription(mod.description);
        }
    }

    public void SetModItemTexture(Mod mod, Texture2D texture) {
        foreach (Transform rowElement in tree.modsRows.transform) {
            var itemTree = rowElement.GetComponent<ModItemTree>();
            if (mod.previewPath.Equals(itemTree.Key)) {
                itemTree.SetPreviewTexture(texture);
            }
        }
    }
}