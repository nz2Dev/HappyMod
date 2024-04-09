using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppLoader : MonoBehaviour {

    [SerializeField] private RootScope rootScopeData;
    [SerializeField] private Canvas mainCanvas;

    private void Start() {
        Application.targetFrameRate = 60;
        Instantiate(rootScopeData).Router(mainCanvas).OnAttached();
    }
}
