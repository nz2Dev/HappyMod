using TMPro;
using UnityEngine;

public class ModItemTree : MonoBehaviour {
    
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    public void SetTitle(string title) {
        titleText.text = title;
    }

    public void SetDescription(string description) {
        descriptionText.text = description;
    }

}