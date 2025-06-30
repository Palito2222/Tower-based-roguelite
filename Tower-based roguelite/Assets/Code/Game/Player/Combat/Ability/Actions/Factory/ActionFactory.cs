using UnityEngine;

public static class ActionFactory
{
    public static IAction Create(ActionData data, GameObject owner)
    {
        switch (data.type)
        {
            case "PlayVFX": return new PlayVFXAction(data, owner);
            case "Hit": return new HitAction(data, owner);
            default: return null;
        }
    }
}