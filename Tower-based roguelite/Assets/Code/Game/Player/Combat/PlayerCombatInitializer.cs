using UnityEngine;
using System.Collections.Generic;
using Game.Player.Combat;

public class PlayerCombatInitializer : MonoBehaviour
{
    public PlayerCombatController combatController;

    public void InitializeFromLoadout(List<int> skillIDs)
    {
        ComboData comboData = ScriptableObject.CreateInstance<ComboData>();
        comboData.comboSteps = new List<ComboStep>();

        foreach (int id in skillIDs)
        {
            ComboStep step = new ComboStep
            {
                attack = new AttackAction { skillID = id }
            };
            comboData.comboSteps.Add(step);
        }

        combatController.SetCombo(comboData);
    }
}