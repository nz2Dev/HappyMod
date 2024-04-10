using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TabBarElement : MonoBehaviour, IPointerClickHandler {

    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI label;

    public event Action<TabBarElement> OnClicked;

    public void OnPointerClick(PointerEventData eventData) {
        OnClicked?.Invoke(this);
    }

    public void SetTintColor(Color color) {
        icon.color = color;
        label.color = color;
    }

}
