using FishNet.Object;
using UnityEngine;

public class PlayerLobbyCharacter : NetworkBehaviour
{
    public override void OnStartClient()
    {
        base.OnStartClient();
        Debug.Log($"[Lobby] Jugador instanciado: {OwnerId}");

        if (IsOwner)
        {
            // Solo el dueño puede controlar esto
            transform.position = GetRandomSpawnPoint();
        }
    }

    void Start()
    {
        var currentScene = gameObject.scene;
        Debug.Log($"[Client] Estoy en la escena: {currentScene.name}, handle: {currentScene.handle}");
    }


    Vector3 GetRandomSpawnPoint()
    {
        return new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
    }
}