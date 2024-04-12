using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ModsUI {
    
    private readonly ModsTree modsTreePrefab;
    private readonly ModItemTree modItemTreePrefab;

    private ModsTree tree;

    public event Action<string> OnItemDownloadButtonClicked;
    public event Action<string> OnSearchBarInputChanged;
    public event Action<string> OnCategoryBarSelectionChanged;

    public ModsUI(ModsTree modsTreePrefab, ModItemTree modItemTreePrefab) {
        this.modsTreePrefab = modsTreePrefab;
        this.modItemTreePrefab = modItemTreePrefab;
    }

    public void SpawnUIElements(RectTransform placementSlot) {
        tree = GameObject.Instantiate(modsTreePrefab.gameObject, placementSlot).GetComponent<ModsTree>();
        tree.searchBar.OnInputChanged += (newValue) => OnSearchBarInputChanged?.Invoke(newValue);
        tree.categoryBar.OnCategorySelectionChanged += (category) => OnCategoryBarSelectionChanged?.Invoke(category);
    }

    public void Show() {
        tree.gameObject.SetActive(true);
    }

    public void Hide() {
        tree.gameObject.SetActive(false);
    }

    public void DeleteUIElements() {
        GameObject.Destroy(tree.gameObject);
    }

    public void SetCategories(string[] categories) {
        tree.categoryBar.SetCategories(categories);
    }

    public void SetNoResultMessageDisplay(bool display) {
        tree.noResultMessage.gameObject.SetActive(display);
    }

    public void SetModItems(ModItem[] modItems) {
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
            modItemTree.SetDownloadingState(modItem.DownloadingProgress);
            modItemTree.OnDownloadClick += (key) => OnItemDownloadButtonClicked?.Invoke(key);
        }
    }

    public void UpdateModItem(ModItem modItem) {
        foreach (Transform rowElement in tree.modsRows.transform) {
            var modItemTree = rowElement.GetComponent<ModItemTree>();
            if (modItem.Key.Equals(modItemTree.Key)) {
                modItemTree.SetTitle(modItem.Data.title);
                modItemTree.SetDescription(modItem.Data.description);
                modItemTree.SetPreviewTexture(modItem.PreviewImage);
                modItemTree.SetDownloadingState(modItem.DownloadingProgress);
            }
        }
    }

}