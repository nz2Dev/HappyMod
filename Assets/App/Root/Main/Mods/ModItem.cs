using UnityEngine;

public class ModItem {
    
    private readonly Mod modData;

    public Mod Data => modData;
    public Texture2D PreviewImage { get; set; }
    public string Key => modData.filePath;

    public ModItem(Mod modData) {
        this.modData = modData;
    }
}