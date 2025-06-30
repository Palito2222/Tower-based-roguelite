using UnityEngine;

public class AnimationEventBridge : MonoBehaviour
{
    public void OnHitEvent(string timing)
    {
        var combat = GetComponentInParent<PlayerCombat>();
        if (combat != null)
        {
            AbilityExecutor.ExecuteActionAtTiming(timing, combat.gameObject); // Pasamos el gameObject Raiz correcto
        }
    }
}
