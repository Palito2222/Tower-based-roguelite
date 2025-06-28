using System;
using UnityEngine;

[Serializable]
public class AttackAction
{
    public int skillID;

    public void Execute(Transform origin, GameObject attacker)
    {
        SkillInfo skill = ConfigManager.Instance.GetByID<SkillInfo>(skillID);
        if (skill == null)
        {
            Debug.LogError($"Skill ID {skillID} no encontrada.");
            return;
        }

        Debug.Log($"[{attacker.name}] usó: {skill.skillName} ({skill.damage} daño)");
        // Aquí puedes añadir VFX, animación y lógica real de daño

        // Simular animación por ahora
        // attacker.GetComponent<Animator>()?.Play(skill.animationName);
    }
}