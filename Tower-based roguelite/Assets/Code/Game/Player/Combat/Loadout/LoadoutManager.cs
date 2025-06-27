using System.Collections.Generic;

public class LoadoutManager
{
    private List<int> equippedSkills = new(); // IDs de skills equipadas

    public void SetLoadout(List<int> skills)
    {
        equippedSkills = skills;
    }

    public List<int> GetLoadout()
    {
        return equippedSkills;
    }
}