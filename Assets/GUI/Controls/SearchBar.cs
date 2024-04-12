using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SearchBar : MonoBehaviour {

    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Button closeButton;

    public event Action<string> OnInputChanged;

    private void Start() {
        closeButton.onClick.AddListener(CloseButtonOnClick);
        inputField.onValueChanged.AddListener(InputFieldOnValueChanged);
    }

    private void InputFieldOnValueChanged(string newValue) {
        OnInputChanged?.Invoke(newValue);
    }

    private void CloseButtonOnClick() {
        inputField.text = "";
    }

}
