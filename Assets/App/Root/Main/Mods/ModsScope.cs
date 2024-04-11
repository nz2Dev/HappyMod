using UnityEngine;

[CreateAssetMenu(fileName = "ModsScope", menuName = "Scopes/Mods")]
public class ModsScope : ScriptableObject {

    [SerializeField] private ModsTree modsTreePrefab;

    public ModsRouter Router(RectTransform placementSlot) {
        return new ModsRouter(
            placementSlot,
            new ModsUI(modsTreePrefab),
            new ModsInteractor(new ModRepository())
        );
    }
    
}