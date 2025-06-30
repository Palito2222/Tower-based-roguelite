using UnityEngine;

public class PlayVFXAction : IAction
{
    private ActionData data;
    private GameObject owner;

    public PlayVFXAction(ActionData d, GameObject o) { data = d; owner = o; }
    public void Prepare() { /* nada */ }
    public void Execute()
    {
        // spawnear VFX desde pool
    }
}