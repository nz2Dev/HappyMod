using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CategoryBarElement : MonoBehaviour {

    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI label;

    private string categoryName;

    public string CategoryName => categoryName;

    public event Action<CategoryBarElement> OnClicked;

    private void Start() {
        button.onClick.AddListener(() => {
            OnClicked?.Invoke(this);
        });
    }

    public void SetCategory(string categoryName) {
        this.categoryName = categoryName;
        label.text = categoryName;
    }

    public void SetStateGraphic(Sprite sprite) {
        button.image.sprite = sprite;
    }

}
