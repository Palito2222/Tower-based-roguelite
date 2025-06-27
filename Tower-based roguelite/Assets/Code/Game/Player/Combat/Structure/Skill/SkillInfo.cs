using System;

[Serializable]
public class SkillInfo : IConfigData
{
    public int skillID;
    public string skillName;
    public string description;
    public float cooldown;
    public int power;
    public string effect;

    public int ID => skillID;
}