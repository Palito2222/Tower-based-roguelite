using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

public class ConfigManager : MonoBehaviour
{
    public static ConfigManager Instance { get; private set; }

    private Dictionary<Type, object> configCache = new();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public async void LoadConfigAsync<T>(string address, Action<Dictionary<int, T>> onComplete) where T : IConfigData
    {
        Type type = typeof(T);

        if (configCache.TryGetValue(type, out object cached))
        {
            onComplete?.Invoke((Dictionary<int, T>)cached);
            return;
        }

        string fulladress = $"Assets/ArrozResources/FileCfg/{address}.json";

        AsyncOperationHandle<TextAsset> handle = Addressables.LoadAssetAsync<TextAsset>(fulladress);
        await handle.Task;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            List<T> list = JsonConvert.DeserializeObject<List<T>>(handle.Result.text);
            Dictionary<int, T> map = new();

            foreach (var entry in list)
                map[entry.ID] = entry;

            configCache[type] = map;
            onComplete?.Invoke(map);
        }
        else
        {
            Debug.LogError($"[ConfigManager] Error al cargar addressable '{fulladress}'");
            onComplete?.Invoke(new Dictionary<int, T>());
        }
    }

    public T GetByID<T>(int id) where T : IConfigData
    {
        var type = typeof(T);
        if (configCache.TryGetValue(type, out object cached))
        {
            var dict = (Dictionary<int, T>)cached;
            return dict.TryGetValue(id, out var result) ? result : default;
        }

        Debug.LogWarning($"[ConfigManager] El tipo {type.Name} no está cargado aún.");
        return default;
    }
}