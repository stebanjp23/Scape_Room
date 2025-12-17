using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;


public class door_controller : MonoBehaviour
{
    [Header("Puerta")]
    public Transform puerta;
    public float openAngle = -90f;       // Cuánto se abre la puerta
    public float speed = 2f;            // Velocidad de apertura
    public bool llave = false;
    private bool isOpen = false;        // Estado de la puerta
    private Quaternion closedRotation;  // Rotación inicial
    private Quaternion openRotation;    // Rotación final
    private bool jugadorEnZona = false; //Ve si el jugador esta en la zona
    private bool tiempoDetenido = false;
    public contador miContador;

    void Start()
    {
        closedRotation = puerta.rotation;
        openRotation = Quaternion.Euler(puerta.eulerAngles + new Vector3(0, openAngle, 0));
    }

    
    void Update()
    {

        if (llave && jugadorEnZona && Keyboard.current.rKey.wasPressedThisFrame && InventarioJugador.TieneLlave) 
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


        if (isOpen && !tiempoDetenido)
        {
            miContador.DetenerContador();
            tiempoDetenido = true;
        }
    }

     private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnZona = true;
            Object.FindFirstObjectByType<MensajesGuia>().MostrarMensaje("Presiona R para abrir la puerta", 3f);
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
