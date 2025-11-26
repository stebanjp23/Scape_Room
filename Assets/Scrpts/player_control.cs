using UnityEngine;
using UnityEngine.InputSystem;

public class player_control : MonoBehaviour
{
    public float speed = 5f;
    public float sensitivity = 2f;
    public Camera playerCamera;

    private CharacterController controller;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private float xRotation = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Estas funciones son llamadas automáticamente por el Player Input
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }

    void Update()
    {

        if (Cursor.lockState == CursorLockMode.Locked) 
        {
            // Movimiento
            Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
            controller.SimpleMove(move * speed);
            // Rotación del mouse
            float mouseX = lookInput.x * sensitivity;
            float mouseY = lookInput.y * sensitivity;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -80f, 80f);

            playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
            transform.Rotate(Vector3.up * mouseX);
        }
    }
}
