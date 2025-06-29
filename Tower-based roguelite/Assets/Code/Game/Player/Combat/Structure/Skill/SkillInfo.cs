using System;

[Serializable]
public class SkillInfo
{
    public int skillID;
    public string skillName;
    public string skillDesc;
    public int skillType;
    public float cooldown;
    public int maxLevel;
    public string executorType;
    public string effectPrefabPath;
    public string iconPath;
}