using UnityEngine;

public class HitAction : IAction
{
    private ActionData data;
    private GameObject owner;

    public HitAction(ActionData d, GameObject o) { data = d; owner = o; }
    public void Prepare() { }
    public void Execute()
    {
        var hits = HitScanner.Scan(data, owner.transform);
        foreach (var h in hits)
        {
            var context = new HitContext(owner, ((MonoBehaviour)h).gameObject, data);
            h.ApplyHit(context);
        }
    }
}