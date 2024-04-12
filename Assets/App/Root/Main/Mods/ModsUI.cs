using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ModsUI {
    
    private readonly ModsTree modsTreePrefab;
    private readonly ModItemTree modItemTreePrefab;

    private ModsTree tree;

    public event Action OnItemDownloadButtonClicked;

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

    public void SetModItems(IEnumerable<ModItem> modItems) {
        var rowsChilds = tree.modsRows.transform.Cast<Transform>().ToArray();
        foreach (var rowChild in rowsChilds) {
            GameObject.Destroy(rowChild.gameObject);
        }

        foreach (var modItem in modItems) {
            var modItemTree = GameObject.Instantiate(modItemTreePrefab.gameObject, tree.modsRows.transform).GetComponent<ModItemTree>();
            modItemTree.SetKey(modItem.Key);
            modItemTree.SetTitle(modItem.Data.title);
            modItemTree.SetDescription(modItem.Data.description);
            modItemTree.SetPreviewTexture(modItem.PreviewImage);
        }
    }

    public void UpdateModItem(ModItem modItem) {
        foreach (Transform rowElement in tree.modsRows.transform) {
            var modItemTree = rowElement.GetComponent<ModItemTree>();
            if (modItem.Key.Equals(modItemTree.Key)) {
                modItemTree.SetTitle(modItem.Data.title);
                modItemTree.SetDescription(modItem.Data.description);
                modItemTree.SetPreviewTexture(modItem.PreviewImage);
            }
        }
    }
}