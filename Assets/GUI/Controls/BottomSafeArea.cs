using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BottomSafeArea : UIBehaviour {
    
    private VerticalLayoutGroup verticalLayoutGroup;

    protected override void Start() {
        verticalLayoutGroup = GetComponent<VerticalLayoutGroup>();
        var rectTransform = (RectTransform) transform;
        var safeAreaPixelVector = new Vector2(0, Screen.safeArea.yMin);
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, safeAreaPixelVector, null, out var safeAreaRectVector)) {
            verticalLayoutGroup.padding.bottom = (int) safeAreaRectVector.y;
        }
    }

}
