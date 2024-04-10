using UnityEngine;

[CreateAssetMenu(fileName = "MainScopeData", menuName = "Scopes/Main")]
public class MainScope : ScriptableObject {
    
    [SerializeField] private ModsScope modsScopeData;
    [SerializeField] private GameObject placeholderTreePrefab;
    [Space]
    [SerializeField] private MainTree screenTreePrefab;

    public MainRouter Router(RectTransform placementSlot) {
        return new MainRouter(
            placementSlot,
            new MainUI(screenTreePrefab),
            new MainInteractor(),
            this
        );
    }

    public ModsScope ModsScope() {
        return Instantiate(modsScopeData);
    }

    public GameObject PlaceholderTreePrfab() {
        return placeholderTreePrefab;
    }

}