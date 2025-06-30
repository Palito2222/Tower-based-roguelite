using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Newtonsoft.Json;
using System.Collections.Generic;
public static class AddressableLoader
{
    /// <summary>
    /// Carga un archivo JSON y lo deserializa en un objeto de tipo T
    /// </summary>
    public static async Task<T> LoadJsonAsync<T>(string address)
    {
        var handle = Addressables.LoadAssetAsync<TextAsset>(address);
        await handle.Task;

        if (handle.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
        {
            string json = handle.Result.text;
            return JsonConvert.DeserializeObject<T>(json);
        }
        else
        {
            Debug.LogError($"[AddressablesJsonLoader] Error al cargar JSON desde: {address}");
            return default;
        }
    }

    /// <summary>
    /// Carga un archivo JSON de array como lista de T (por ejemplo: CharacterBase.json)
    /// </summary>
    public static async Task<List<T>> LoadJsonListAsync<T>(string address)
    {
        var handle = Addressables.LoadAssetAsync<TextAsset>(address);
        await handle.Task;

        if (handle.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
        {
            string json = handle.Result.text;
            return JsonConvert.DeserializeObject<List<T>>(json);
        }
        else
        {
            Debug.LogError($"[AddressablesJsonLoader] Error al cargar JSON lista desde: {address}");
            return new List<T>();
        }
    }
}
