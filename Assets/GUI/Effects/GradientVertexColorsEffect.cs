using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GradientVertexColorsEffect : BaseMeshEffect {

    [SerializeField] private Color start = Color.white;
    [SerializeField] private Color end = Color.black;

    public override void ModifyMesh(VertexHelper vh) {
        var vertex = default (UIVertex);
        for (int i = 0; i < vh.currentVertCount; i++) {
           if (i == 1 || i == 2) {
                vh.PopulateUIVertex(ref vertex, i);
                vertex.color = start;
                vh.SetUIVertex(vertex, i);
           } else {
                vh.PopulateUIVertex(ref vertex, i);
                vertex.color = end;
                vh.SetUIVertex(vertex, i);
           }
        }
    }
}
