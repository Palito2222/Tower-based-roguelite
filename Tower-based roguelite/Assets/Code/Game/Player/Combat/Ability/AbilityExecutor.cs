using UnityEngine;

public class AbilityExecutor
{
    public static void ExecuteComboStep(ComboStep step, GameObject owner)
    {
        foreach (var action in step.actions)
        {
            var a = ActionFactory.Create(action, owner);
            a?.Prepare(); // por si es delay
        }
    }

    public static void ExecuteActionAtTiming(string timing, GameObject owner)
    {
        var currentStep = owner.GetComponentInParent<PlayerCombat>().CurrentComboStep;
        foreach (var action in currentStep.actions)
        {
            if (action.timing == timing)
            {
                var a = ActionFactory.Create(action, owner);
                a?.Execute(); // este es el hit real
            }
        }
    }
}