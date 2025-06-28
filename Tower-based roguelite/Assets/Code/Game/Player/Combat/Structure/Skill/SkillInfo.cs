using System;

[Serializable]
public class SkillInfo : IConfigData
{
    public int skillID;
    public string skillName;
    public string description;
    public float damage;
    public float cooldown;
    public float comboWindow;
    public string animationName;

    public int ID => skillID;
}