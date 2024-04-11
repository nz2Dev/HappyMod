using UnityEngine;

[CreateAssetMenu(fileName = "ModsScope", menuName = "Scopes/Mods")]
public class ModsScope : ScriptableObject {

    [SerializeField] private ModsTree modsTreePrefab;
    [SerializeField] private ModItemTree modItemTreePrefab;

    public ModsRouter Router(RectTransform placementSlot) {
        return new ModsRouter(
            placementSlot,
            new ModsUI(modsTreePrefab, modItemTreePrefab),
            new ModsInteractor(new ModRepository())
        );
    }
    
}