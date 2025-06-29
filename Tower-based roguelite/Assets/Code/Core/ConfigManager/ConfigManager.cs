using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;

public class ConfigManager : MonoBehaviour
{
    public static List<CharacterBaseData> Characters;
    public static List<CharacterAbilityData> CharacterAbilities;

    public static async Task LoadCoreConfigsAsync()
    {
        Characters = await LoadJsonList<CharacterBaseData>("Assets/ArrozResources/FileCfg/CharacterBase.json");
        CharacterAbilities = await LoadJsonList<CharacterAbilityData>("Assets/ArrozResources/FileCfg/CharacterAbilityConfig.json");
    }

    public static async Task<AbilityData> LoadAbilityAsync(string characterName, string abilityScript)
    {
        string path = $"Assets/ArrozResources/Data/Abilities/CharacterAbility/{characterName}/{abilityScript}.json";
        return await LoadJson<AbilityData>(path);
    }

    private static async Task<T> LoadJson<T>(string path)
    {
        var handle = Addressables.LoadAssetAsync<TextAsset>(path);
        await handle.Task;

        if (handle.Status != UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
        {
            Debug.LogError($"Failed to load JSON at path: {path}");
            return default;
        }

        string json = handle.Result.text;
        return JsonConvert.DeserializeObject<T>(json);
    }

    private static async Task<List<T>> LoadJsonList<T>(string path)
    {
        var handle = Addressables.LoadAssetAsync<TextAsset>(path);
        await handle.Task;

        if (handle.Status != UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
        {
            Debug.LogError($"Failed to load JSON list at path: {path}");
            return new List<T>();
        }

        string json = handle.Result.text;

        try
        {
            // Si es un array JSON directo
            return JsonConvert.DeserializeObject<List<T>>(json);
        }
        catch (JsonSerializationException ex)
        {
            Debug.LogError($"JSON format error at {path}: {ex.Message}");
            return new List<T>();
        }
    }
}