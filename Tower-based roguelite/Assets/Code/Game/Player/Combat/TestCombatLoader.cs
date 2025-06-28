using UnityEngine;
using System.Collections.Generic;

public class TestCombatLoader : MonoBehaviour
{
    public PlayerCombatInitializer initializer;

    void Start()
    {
        // Cargar configs primero
        ConfigManager.Instance.LoadConfigAsync<SkillInfo>("SkillBase", OnSkillsReady);
    }

    void OnSkillsReady(Dictionary<int, SkillInfo> skillMap)
    {
        // Simular loadout de Kaoru
        List<int> kaoruSkills = new() { 100101, 100102, 100103 };
        initializer.InitializeFromLoadout(kaoruSkills);
    }
}