using UnityEngine;

public class AnimationEventBridge : MonoBehaviour
{
    public void OnHitEvent(string timing)
    {
        AbilityExecutor.ExecuteActionAtTiming(timing, gameObject);
    }
}
