using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.AddressableAssets;

public static class ConfigManager
{
    private static Dictionary<string, object> cache = new();

    /// <summary>
    /// Carga un archivo JSON de lista (ej: CharacterBase, SkillConfig, etc.)
    /// </summary>
    public static async Task<List<T>> LoadListAsync<T>(string address)
    {
        if (cache.TryGetValue(address, out var cached) && cached is List<T> list)
            return list;

        var handle = Addressables.LoadAssetAsync<TextAsset>(address);
        await handle.Task;

        if (handle.Status != UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
        {
            Debug.LogError($"[ConfigManager] Error cargando lista desde: {address}");
            return new List<T>();
        }

        var json = handle.Result.text;
        var result = JsonConvert.DeserializeObject<List<T>>(json);
        cache[address] = result;
        return result;
    }

    /// <summary>
    /// Carga un archivo JSON simple (objeto único, como una habilidad individual)
    /// </summary>
    public static async Task<T> LoadAsync<T>(string address)
    {
        if (cache.TryGetValue(address, out var cached) && cached is T obj)
            return obj;

        var handle = Addressables.LoadAssetAsync<TextAsset>(address);
        await handle.Task;

        if (handle.Status != UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
        {
            Debug.LogError($"[ConfigManager] Error cargando archivo: {address}");
            return default;
        }

        var json = handle.Result.text;
        var result = JsonConvert.DeserializeObject<T>(json);
        cache[address] = result;
        return result;
    }

    /// <summary>
    /// Borra el caché si quieres forzar recarga
    /// </summary>
    public static void ClearCache() => cache.Clear();
}