using System.Collections.Generic;

[System.Serializable]
public class CharacterData
{
    public int id;
    public string displayName;
    public float maxHealth;

    public string comboId;                  // Referencia a ComboData
    public List<string> abilityIds;         // IDs de habilidades activas
}
