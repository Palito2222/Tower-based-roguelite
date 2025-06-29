using Game.Player.Combat;
using UnityEngine;

public class TestCombatLoader : MonoBehaviour
{
    public PlayerCombatController combatController;

    async void Start()
    {
        var kaoru = await ConfigManager.Instance.GetAsync<CharacterInfo>("CharacterBase", 1001);

        ComboData combo = ScriptableObject.CreateInstance<ComboData>();
        combo.comboSteps = new();

        foreach (var skillId in kaoru.defaultSkillIDs)
        {
            var step = new ComboStep { skillID = skillId, comboWindow = 0.5f };
            step.skillInfo = await ConfigManager.Instance.GetAsync<SkillInfo>("SkillInfo", skillId);

            if (step.skillInfo == null)
            {
                Debug.LogError($"SkillInfo no encontrada para SkillId: {skillId}");
                continue;
            }

            step.executor = SkillExecutorRegistry.GetExecutor(step.skillInfo.executorType);
            combo.comboSteps.Add(step);
        }

        combatController.SetCombo(combo);
    }
}