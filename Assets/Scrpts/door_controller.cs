using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;


public class door_controller : MonoBehaviour
{
    [Header("Puerta")]
    public Transform puerta;
    public float openAngle = -90f;       // Cuánto se abre la puerta
    public float speed = 2f;            // Velocidad de apertura
    private bool isOpen = false;        // Estado de la puerta
    private Quaternion closedRotation;  // Rotación inicial
    private Quaternion openRotation;    // Rotación final
    private bool jugadorEnZona = false; //Ve si el jugador esta en la zona

    void Start()
    {
        closedRotation = puerta.rotation;
        openRotation = Quaternion.Euler(puerta.eulerAngles + new Vector3(0, openAngle, 0));
    }

    // Update is called once per frame
    void Update()
    {
        if (jugadorEnZona && Keyboard.current.rKey.wasPressedThisFrame && InventarioJugador.TieneLlave) // Suponiendo que hay un InventarioJugador estático
        {
            isOpen = true;
            Debug.Log("Puerta abierta con llave.");
        }

        if (isOpen)
        {
            puerta.rotation = Quaternion.Lerp(puerta.rotation, openRotation, Time.deltaTime * speed);
        }
        else
        {
            puerta.rotation = Quaternion.Lerp(puerta.rotation, closedRotation, Time.deltaTime * speed);
        }
    }

     private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnZona = true;
            Debug.Log("Acércate y presiona R para abrir la puerta.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnZona = false;
        }
    }
}
