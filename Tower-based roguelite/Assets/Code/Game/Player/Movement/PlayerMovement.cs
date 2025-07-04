using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
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


    private void Start()
    {
        controller = GetComponent<CharacterController>();
        vc = GameObject.Find("PlayerCamera").GetComponent<CinemachineVirtualCamera>();

        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;

        vc.Follow = cameraLookPoint;
        vc.LookAt = cameraLookPoint;
    }

    private void Update()
    {
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
