using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SafeAreaOffsets : UIBehaviour {
    
    [SerializeField] private bool trueTopFalseBottom = false;

    private VerticalLayoutGroup verticalLayoutGroup;

    protected override void Start() {
        verticalLayoutGroup = GetComponent<VerticalLayoutGroup>();
        var rectTransform = (RectTransform) transform;
        var safeAreaPixelVector = new Vector2(0, trueTopFalseBottom ? (Screen.height - Screen.safeArea.yMax) : Screen.safeArea.yMin);
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, safeAreaPixelVector, null, out var safeAreaRectVector)) {
            if (trueTopFalseBottom) {
                verticalLayoutGroup.padding.top = (int) safeAreaRectVector.y;
            } else {
                verticalLayoutGroup.padding.bottom = (int) safeAreaRectVector.y;
            }
        }
    }

}
