using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ModItemTree : MonoBehaviour {
    
    [SerializeField] private Image previewImage;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    private string key;

    public string Key => key;

    public void SetKey(string key) {
        this.key = key;
    }

    public void SetTitle(string title) {
        titleText.text = title;
    }

    public void SetDescription(string description) {
        descriptionText.text = description;
    }

    public void SetPreviewTexture(Texture2D texture) {
        previewImage.sprite = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), Vector2.zero);
    }
}