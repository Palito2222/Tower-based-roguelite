using System;
using System.Collections.Generic;

[Serializable]
public class CharacterInfo : IConfigData
{
    public int characterID;
    public string name;
    public string description;
    public List<int> defaultSkillIDs;
    public List<int> unlockableSkillIDs;
    public string element;
    public string role;

    public int ID => characterID;
}