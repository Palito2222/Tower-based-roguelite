using UnityEngine;

public interface ISkillExecutor
{
    void Execute(SkillInfo info, Transform origin, GameObject caster);
}
