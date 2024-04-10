using System;
using UnityEngine;

public class MainUI {

    public enum Tab {
        Mods,
        Placeholder1,
        Placeholder2,
        Placeholder3,
    }

    private readonly MainTree treePrefab;

    private MainTree tree;

    public RectTransform ContentContainer => tree.contentContainer;

    public event Action<Tab> OnTabClicked;

    public MainUI(MainTree treePrefab) {
        this.treePrefab = treePrefab;
    }

    public void SpawnUIElements(RectTransform placementSlot) {
        tree = GameObject.Instantiate(treePrefab.gameObject, placementSlot).GetComponent<MainTree>();

        tree.tabBar.OnTabClicked += (tabElement) => {
            OnTabClicked?.Invoke(TabElementToEnum(tabElement));
        };
    }

    public void SetTabSelected(Tab tabEnum) {
        tree.tabBar.SetTabSelected(TabEnumToElement(tabEnum));
    }

    private Tab TabElementToEnum(TabBarElement tabElement) {
        if (tabElement == tree.modsTab) {
            return Tab.Mods;
        } else if (tabElement == tree.placeholder1Tab) {
            return Tab.Placeholder1;
        } else if (tabElement == tree.placeholder2Tab) {
            return Tab.Placeholder2;
        } else if (tabElement == tree.placeholder3Tab) {
            return Tab.Placeholder3;
        } else {
            throw new Exception(tabElement.ToString());
        }
    }

    private TabBarElement TabEnumToElement(Tab tabEnum) {
        if (tabEnum == Tab.Mods) {
            return tree.modsTab;
        } else if (tabEnum == Tab.Placeholder1) {
            return tree.placeholder1Tab;
        } else if (tabEnum == Tab.Placeholder2) {
            return tree.placeholder2Tab;
        } else if (tabEnum == Tab.Placeholder3) {
            return tree.placeholder3Tab;
        } else {
            throw new Exception(tabEnum.ToString());
        }
    }

}