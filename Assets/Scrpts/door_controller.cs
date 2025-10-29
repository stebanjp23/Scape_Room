using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;


public class door_controller : MonoBehaviour
{
    public float openAngle = -180f;       // Cuánto se abre la puerta
    public float speed = 2f;            // Velocidad de apertura
    private bool isOpen = false;        // Estado de la puerta
    private Quaternion closedRotation;  // Rotación inicial
    private Quaternion openRotation;    // Rotación final

    void Start()
    {
        closedRotation = transform.rotation;
        openRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, openAngle, 0));
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.rKey.wasPressedThisFrame) // Cuando presiones "E"
        {
            isOpen = !isOpen; // Cambia el estado (abrir/cerrar)
        }

        if (isOpen)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, openRotation, Time.deltaTime * speed);
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, closedRotation, Time.deltaTime * speed);
        }
    }
}
