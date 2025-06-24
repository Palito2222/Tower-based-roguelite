using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ConfigManager : MonoBehaviour
{
    public static ConfigManager Instance { get; private set; }

    private Dictionary<string, string> configCache = new Dictionary<string, string>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public T LoadConfig<T>(string fileName)
    {
        if (!configCache.ContainsKey(fileName))
        {
            string path = Path.Combine(Application.dataPath, "ArrozResources/FileCfg/", fileName + ".json");
            if (File.Exists(path))
                configCache[fileName] = File.ReadAllText(path);
            else
                Debug.LogError($"[ConfigManager] Archivo no encontrado: {path}");
        }

        return JsonUtility.FromJson<T>(configCache[fileName]);
    }
}
