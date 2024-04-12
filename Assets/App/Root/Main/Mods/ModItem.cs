using UnityEngine;

public class ModItem {
    
    private readonly Mod modData;

    public Mod Data => modData;
    public string Key => modData.filePath;

    public Texture2D PreviewImage { get; set; }
    public bool DownloadingProgress { get; set; }

    public ModItem(Mod modData) {
        this.modData = modData;
    }
}