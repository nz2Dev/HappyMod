using UnityEngine;

[CreateAssetMenu(menuName = "Scopes/Root")]
public class RootScope : ScriptableObject {
    
    [SerializeField] private LoadingScope loadingScopeData;
    [SerializeField] private MainScope mainScopeData;
    [Space]
    [SerializeField] private AlertControl alertControlPrefab;

    public RootRouter Router(Canvas mainCanvas) {
        return new RootRouter(
            mainCanvas, 
            alertControlPrefab, 
            new RootInteractor(), 
            this
        );
    }

    public LoadingScope LoadingScope() {
        return Instantiate(loadingScopeData);
    }

    public MainScope MainScope() {
        return Instantiate(mainScopeData);
    }

}