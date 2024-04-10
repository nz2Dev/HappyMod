using UnityEngine;

[CreateAssetMenu(fileName = "ModsScope", menuName = "Scopes/Mods")]
public class ModsScope : ScriptableObject {
    [SerializeField] private GameObject modsTreePrefab;

    public ModsRouter Router(RectTransform placementSlot) {
        return new ModsRouter(
            placementSlot,
            modsTreePrefab
        );
    }
}