using System;
using UnityEngine;

public class MainUI {

    private readonly MainTree treePrefab;

    private MainTree tree;

    public MainUI(MainTree treePrefab) {
        this.treePrefab = treePrefab;
    }

    public void SpawnUIElements(RectTransform placementSlot) {
        tree = GameObject.Instantiate(treePrefab.gameObject, placementSlot).GetComponent<MainTree>();
    }

}