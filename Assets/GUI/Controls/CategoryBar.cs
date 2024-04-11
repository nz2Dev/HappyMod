using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Events;
using System;
using Unity.VisualScripting;

public class CategoryBar : MonoBehaviour {
    
    [SerializeField] private CategoryBarElement categoryPrefab;
    [SerializeField] private Sprite unselectedStateSprite;
    [SerializeField] private Sprite selectedStateSprite;
    [Space]
    [SerializeField] private string[] previewCategories = new string[] {
        "Category 1",
        "Category 2",
        "Category 3"
    };
    
    public event Action<string> OnCategoryClick;

    private void Start() {
        SetCategories(previewCategories);
    }

    public void SetCategories(string[] categories) {
        var childs = transform.Cast<Transform>().ToArray();
        foreach (var child in childs) {
            Destroy(child.gameObject);
        }

        foreach (var category in categories) {
            var categoryElement = Instantiate(categoryPrefab.gameObject, transform).GetComponent<CategoryBarElement>();
            categoryElement.SetCategory(category);
            categoryElement.OnClicked += (element) => {
                OnCategoryClick?.Invoke(element.CategoryName);
                SetSelectedCategory(element.CategoryName);
            };
        }
    }

    public void SetSelectedCategory(string category) {
        foreach (Transform child in transform) {
            var categoryElement = child.GetComponent<CategoryBarElement>();
            if (category.Equals(categoryElement.CategoryName)) {
                categoryElement.SetStateGraphic(selectedStateSprite);
            } else {
                categoryElement.SetStateGraphic(unselectedStateSprite);
            }
        }
    }

}
