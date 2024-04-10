using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabBar : MonoBehaviour {
    
    [SerializeField] private TabBarElement[] tabs;
    [SerializeField] private Color selectedColor;
    [SerializeField] private Color unselectedColor;

    public IEnumerable<TabBarElement> Tabs => tabs;

    public event Action<TabBarElement> OnTabClicked;

    private void Awake() {
        foreach (var tab in tabs) {
            tab.OnClicked += (clickedElement) => {
                SetTabSelected(clickedElement);
                OnTabClicked?.Invoke(clickedElement);
            };
        }
    }

    public void SetTabSelected(TabBarElement element) {
        foreach (var tab in tabs) {
            if (tab == element) {
                tab.SetTintColor(selectedColor);
            } else {
                tab.SetTintColor(unselectedColor);
            }
        }
    }

}
