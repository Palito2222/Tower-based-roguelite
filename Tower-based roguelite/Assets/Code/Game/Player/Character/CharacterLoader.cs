using Game.Player.Combat;
using UnityEngine;

public class CharacterLoader : MonoBehaviour
{
    [SerializeField] private int characterId;

    public CharacterData LoadedData { get; private set; }

    public void LoadCharacter()
    {
        LoadedData = ConfigManager.Instance.LoadConfig<CharacterData>("Characters/" + characterId);

        // Aplicar vida, velocidad...
        var movement = GetComponent<PlayerMovement>();

        var combat = GetComponent<PlayerCombatController>();
        var combo = ConfigManager.Instance.LoadConfig<ComboData>("Combos/" + LoadedData.comboId);
        combat.SetCombo(combo);

        // Enlazar habilidades más adelante...
    }

    private void Start()
    {
        LoadCharacter();
    }
}