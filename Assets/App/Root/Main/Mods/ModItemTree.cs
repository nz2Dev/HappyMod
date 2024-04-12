using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ModItemTree : MonoBehaviour {
    
    [SerializeField] private Image previewImage;
    [SerializeField] private RectTransform previewProgressBar;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Button downloadButton;
    [SerializeField] private RectTransform progressBar;

    private string key;

    public string Key => key;

    public event Action<string> OnDownloadClick;

    private void Start() {
        downloadButton.onClick.AddListener(() => OnDownloadClick?.Invoke(key));
    }

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
        previewImage.gameObject.SetActive(texture != null);
        previewProgressBar.gameObject.SetActive(texture == null);
        
        if (texture != null) {
            previewImage.sprite = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), Vector2.zero);
        }
    }

    public void SetDownloadingState(bool downloading) {
        downloadButton.gameObject.SetActive(!downloading);
        progressBar.gameObject.SetActive(downloading);
    }
}