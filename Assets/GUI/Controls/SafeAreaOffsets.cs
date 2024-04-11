using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// This script assumes that in case of top offset:
// - the pivot is in the top left corner (0, 1)
// - after the layout, element is in the top most left corner of the screen
// and in case of bottom offset:
// - the pivot is in the bottom left corner (0, 0)
// - after the layout, element is in the bottom most left corner of the screen 
public class SafeAreaOffsets : MonoBehaviour {
    
    [SerializeField] private bool trueTopFalseBottom = false;

    private IEnumerator Start() {
        yield return null;
        UpdateOffset();
    }

    private void UpdateOffset() {
        var rectTransform = (RectTransform) transform;
        var verticalLayoutGroup = GetComponent<VerticalLayoutGroup>();
        
        var screenPoint = new Vector2(0, trueTopFalseBottom ?  Screen.safeArea.yMax : Screen.safeArea.yMin);
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, screenPoint, null, out var localPointInRect)) {
            if (trueTopFalseBottom) {
                verticalLayoutGroup.padding.top = (int) -localPointInRect.y;
                verticalLayoutGroup.padding.bottom = 0;
            } else {
                verticalLayoutGroup.padding.top = 0;
                verticalLayoutGroup.padding.bottom = (int) localPointInRect.y;
            }

            LayoutRebuilder.MarkLayoutForRebuild(rectTransform);
        }
    }

}
