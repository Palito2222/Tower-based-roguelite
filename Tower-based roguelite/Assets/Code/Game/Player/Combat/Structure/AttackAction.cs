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

        Debug.Log($"[{attacker.name}] us�: {skill.skillName} ({skill.damage} da�o)");
        // Aqu� puedes a�adir VFX, animaci�n y l�gica real de da�o

        // Simular animaci�n por ahora
        // attacker.GetComponent<Animator>()?.Play(skill.animationName);
    }
}