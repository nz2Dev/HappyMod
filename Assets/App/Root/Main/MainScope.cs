using UnityEngine;

[CreateAssetMenu(fileName = "MainScopeData", menuName = "Scopes/Main")]
public class MainScope : ScriptableObject {
    
    [SerializeField] private MainTree screenTreePrefab;

    public MainRouter Router(RectTransform placementSlot) {
        return new MainRouter(
            placementSlot,
            new MainUI(screenTreePrefab),
            new MainInteractor(),
            this
        );
    }

}