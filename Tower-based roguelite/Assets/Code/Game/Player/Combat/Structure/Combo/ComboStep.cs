[System.Serializable]
public class ComboStep
{
    public AttackAction attack;
    public float comboWindow =>
        ConfigManager.Instance.GetByID<SkillInfo>(attack.skillID)?.comboWindow ?? 1.0f;
}
