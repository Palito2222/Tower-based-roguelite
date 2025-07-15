using Cinemachine;
using FishNet.Connection;
using FishNet.Object;
using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : NetworkBehaviour
{
    [Title("Movement")]
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float gravity = -14f;
    public float jumpHeight = 1.6f;
    public float rotationSpeed = 10f;

    [Title("Camera")]
    public Transform cameraTransform;
    public Transform cameraLookPoint;
    public CinemachineVirtualCamera vc;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (!IsOwner) return;
        StartCoroutine(DelayedSetup());
    }

    private void OnClientSceneChanged(NetworkConnection conn)
    {
        Debug.Log($"Cliente cargó escena: {UnityEngine.SceneManagement.SceneManager.GetActiveScene().name}");
    }

    private IEnumerator DelayedSetup()
    {
        // Espera hasta que haya una MainCamera válida
        while (Camera.main == null)
            yield return null;

        controller = GetComponent<CharacterController>();

        var camGO = GameObject.Find("PlayerCamera");
        if (camGO != null)
            vc = camGO.GetComponent<CinemachineVirtualCamera>();

        cameraTransform = Camera.main.transform;

        if (vc != null && cameraLookPoint != null)
        {
            vc.Follow = cameraLookPoint;
            vc.LookAt = cameraLookPoint;
        }
    }

    private void Update()
    {
        if (!IsOwner) return;

        HandleMovement();
        ApplyGravity();
    }

    private void HandleMovement()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        // 1. Input de movimiento
        Vector2 input = InputManager.Instance.GetMovementInput();
        Vector3 inputDir = new Vector3(input.x, 0f, input.y).normalized;

        if (inputDir.magnitude >= 0.1f)
        {
            // 2. Convertir input relativo a la cámara
            Vector3 camForward = cameraTransform.forward;
            Vector3 camRight = cameraTransform.right;

            camForward.y = 0;
            camRight.y = 0;
            camForward.Normalize();
            camRight.Normalize();

            Vector3 moveDir = camForward * inputDir.z + camRight * inputDir.x;
            moveDir.Normalize();

            // 3. Movimiento
            float speed = InputManager.Instance.IsRunning() ? runSpeed : walkSpeed;
            controller.Move(moveDir * speed * Time.deltaTime);

            // 4. Rotación suave del personaje hacia la dirección
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // 5. Salto
        if (isGrounded && InputManager.Instance.JumpTriggered())
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    private void ApplyGravity()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
