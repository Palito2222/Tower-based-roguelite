using FishNet;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneDebugTool : MonoBehaviour
{
    public InputAction debugAction;

    private void OnEnable()
    {
        debugAction.Enable();
        debugAction.performed += OnDebugPressed;
    }

    private void OnDisable()
    {
        debugAction.performed -= OnDebugPressed;
        debugAction.Disable();
    }

    private void OnDebugPressed(InputAction.CallbackContext ctx)
    {
        if (!InstanceFinder.IsServerStarted) return;

        int sceneCount = UnityEngine.SceneManagement.SceneManager.sceneCount;
        Debug.Log("========== [FishNet] Escenas activas en el servidor ==========");

        for (int i = 0; i < sceneCount; i++)
        {
            Scene scene = UnityEngine.SceneManagement.SceneManager.GetSceneAt(i);
            if (!scene.isLoaded || !scene.IsValid()) continue;

            if (InstanceFinder.SceneManager.SceneConnections.TryGetValue(scene, out var connections))
            {
                string connIds = connections.Count > 0
                    ? string.Join(", ", connections.Select(c => $"ClientId:{c.ClientId}"))
                    : "Sin jugadores";

                Debug.Log($"→ Escena: {scene.name} | Handle: {scene.handle} | Jugadores: {connIds}");
            }
            else
            {
                Debug.Log($"→ Escena: {scene.name} | Handle: {scene.handle} | [No gestionada por FishNet]");
            }
        }
    }
}
