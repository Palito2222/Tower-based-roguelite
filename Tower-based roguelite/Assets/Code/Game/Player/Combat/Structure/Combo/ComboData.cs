using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ComboData", menuName = "Combat/Combo")]
public class ComboData : ScriptableObject
{
    public List<ComboStep> comboSteps;
}
