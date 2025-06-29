using System;
using UnityEngine;

[Serializable]
public class ComboStep
{
    public int skillID;
    public float comboWindow = 0.5f;

    [NonSerialized] public SkillInfo skillInfo;
    [NonSerialized] public ISkillExecutor executor;

    public async void Initialize()
    {
        skillInfo = await ConfigManager.Instance.GetAsync<SkillInfo>("SkillInfo", skillID, "skillID");
        executor = SkillExecutorRegistry.GetExecutor(skillInfo.executorType);
    }

    public void Execute(Transform origin, GameObject caster)
    {
        executor?.Execute(skillInfo, origin, caster);
    }
}
