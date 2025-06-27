using System.Collections.Generic;
using UnityEngine;

public class LoadoutTest : MonoBehaviour
{
    void Start()
    {
        ConfigManager.Instance.LoadConfigAsync<CharacterInfo>("CharacterBase", OnCharactersLoaded);
    }

    private void OnCharactersLoaded(Dictionary<int, CharacterInfo> chars)
    {
        Debug.Log($"Personajes cargados: {chars.Count}");

        if (chars.TryGetValue(1001, out var kaoru))
        {
            Debug.Log($"Kaoru: {kaoru.name} / Skills por defecto: {string.Join(", ", kaoru.defaultSkillIDs)}");

            // Simular loadout
            LoadoutManager loadout = new();
            loadout.SetLoadout(new List<int> { 100102, 200101, 200102 });

            Debug.Log("Loadout equipado:");
            foreach (var skillID in loadout.GetLoadout())
            {
                var skill = ConfigManager.Instance.GetByID<SkillInfo>(skillID);
                Debug.Log($"SkillID {skillID} → {(skill != null ? skill.skillName : "Skill no cargada aún")}");
            }
        }
    }
}