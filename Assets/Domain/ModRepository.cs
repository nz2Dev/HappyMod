using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugins.Dropbox;
using UnityEngine;

public class ModRepository {

    public async Task DownloadModsConfig() {
        await DropboxHelper.Initialize();
        await DropboxHelper.DownloadAndSaveFile("mods.json");
    }

    public async Task<ModsConfig> GetModsConfig() {
        var cachedModsPath = DropboxHelper.GetDownloadedFilePathInPersistentStorage("mods.json");

        using var jsonReader = new JsonTextReader(new StreamReader(File.OpenRead(cachedModsPath)));
        var configObject = await JObject.LoadAsync(jsonReader);
        
        var mods = configObject["mods"]?.Values<JObject>().Select(modObject => new Mod {
            category = modObject["category"]?.Value<string>(),
            previewPath = modObject["previewPath"]?.Value<string>(),
            filePath = modObject["filePath"]?.Value<string>(),
            title = modObject["title"]?.Value<string>(),
            description = modObject["description"]?.Value<string>(),
        });
        
        var categories = configObject["categories"]?.Values<string>();

        return new ModsConfig {
            mods = mods.ToArray(),
            categories = categories.ToArray()
        };
    }

}