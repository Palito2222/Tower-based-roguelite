using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;

public class ConfigManager : MonoBehaviour
{
    public static ConfigManager Instance { get; private set; }

    private Dictionary<string, object> _cache = new();

    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public async Task<List<T>> GetAllAsync<T>(string adress)
    {
        if (_cache.TryGetValue(adress, out var cached))
            return (List<T>)cached;

        string path = $"Assets/ArrozResources/FileCfg/{adress}.json";

        TextAsset jsonAsset = await Addressables.LoadAssetAsync<TextAsset>(path).Task;
        if (jsonAsset != null)
        {
            Debug.LogError($"[Config Manager] JSON not found: {path}");
            return null;
        }

        List<T> result = JsonConvert.DeserializeObject<List<T>>(jsonAsset.text);
        _cache[path] = result;
        return result;
    }

    public async Task<T> GetAsync<T>(string address, int id, string fieldName = "skillID") where T : class
    {
        List<T> list = await GetAllAsync<T>(address);
        if (list == null) return null;

        foreach (T item in list)
        {
            var field = typeof(T).GetField(fieldName);
            if (field == null)
            {
                Debug.LogError($"[Config Manager] No field '{fieldName}' in {typeof(T).Name}");
                continue;
            }

            if ((int)field.GetValue(item) == id )
                return item;
        }

        return null;
    }
}