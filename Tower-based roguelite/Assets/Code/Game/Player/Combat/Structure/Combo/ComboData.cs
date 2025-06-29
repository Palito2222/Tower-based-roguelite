using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Combat/Combo Data")]
public class ComboData : ScriptableObject
{
    public List<ComboStep> comboSteps;

    public void Initialize()
    {
        foreach (var step in comboSteps)
        {
            step.Initialize();
        }
    }
}
