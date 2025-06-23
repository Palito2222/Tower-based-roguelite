using UnityEngine;
using Cinemachine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    private CinemachineFreeLook cinemachine;

    void Awake()
    {
        cinemachine = GetComponent<CinemachineFreeLook>();
        if (target != null)
            cinemachine.Follow = target;
    }

    void LateUpdate()
    {
        if (cinemachine.Follow != target)
            cinemachine.Follow = target;
    }
}
