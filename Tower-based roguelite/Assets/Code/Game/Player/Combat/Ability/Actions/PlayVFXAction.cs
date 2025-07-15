using UnityEngine;

public class PlayVFXAction : IAction
{
    private ActionData data;
    private GameObject owner;

    public PlayVFXAction(ActionData data, GameObject owner)
    {
        this.data = data;
        this.owner = owner;
    }

    public void Prepare() { }
    public void Execute()
    {
        //GameObject vfxPrefab = VFXDatabase.Get(data.vfx); // puedes usar Addressables aquí

        Transform attachPoint = owner.transform;

        if (!string.IsNullOrEmpty(data.attachTo))
        {
            var animator = owner.GetComponentInChildren<Animator>();
            if (animator) attachPoint = animator.GetBoneTransform(HumanBodyBones.RightHand); // o usa data.attachTo
        }

        Vector3 offset = new Vector3(data.offsetX, data.offsetY, data.offsetZ);
        //GameObject instance = GameObject.Instantiate(vfxPrefab, attachPoint.position + offset, attachPoint.rotation);

        //if (!string.IsNullOrEmpty(data.attachTo))
            //instance.transform.SetParent(attachPoint);

        //if (data.duration > 0)
            //GameObject.Destroy(instance, data.duration);
    }
}
