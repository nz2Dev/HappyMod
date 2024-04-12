using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AlertControl : MonoBehaviour, IPointerClickHandler {

    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Button button;

    public event Action OnOutsideBoxClick;
    public event Action OnButtonClick;

    private void Start() {
        button.onClick.AddListener(() => OnButtonClick?.Invoke());
    }

    public void SetTitle(string title) {
        titleText.text = title;
        titleText.gameObject.SetActive(!string.IsNullOrEmpty(title));
    }

    public void SetMessage(string message) {
        messageText.text = message;
        messageText.gameObject.SetActive(!string.IsNullOrEmpty(message));
    }

    public void SetButton(string label) {
        button.GetComponentInChildren<TextMeshProUGUI>().text = label;
        button.gameObject.SetActive(!string.IsNullOrEmpty(label));
    }

    public void OnPointerClick(PointerEventData eventData) {
        OnOutsideBoxClick?.Invoke();
    }
    
}
