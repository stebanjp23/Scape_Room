using UnityEngine;
using UnityEngine.InputSystem;

public class recolectar_libro : MonoBehaviour
{
// Referencia al Manager central para reportar la recolección
    private libros_mager manager; 
    private bool jugadorEnZona = false; // Bandera que se activa cuando el jugador entra en el trigger

    void Start()
    {
        // Busca el Manager al inicio del juego
        manager = FindFirstObjectByType<libros_mager>();
        if (manager == null)
        {
            Debug.LogError("LibrosManager no encontrado en la escena. Asegúrate de que está adjunto a un objeto.");
        }
    }

    void Update()
    {
        // 1. Verificar si el jugador está en la zona Y ha pulsado la tecla E
        if (jugadorEnZona && Keyboard.current.eKey.wasPressedThisFrame)
        {
            RecogerLibro();
        }
    }

    // Se llama cuando el Jugador (con Rigidbody) entra en el Box Collider (Is Trigger) del libro
    private void OnTriggerEnter(Collider other)
    {
        // Comprueba si el objeto que entró es el Jugador (debe tener la etiqueta "Player")
        if (other.CompareTag("Player"))
        {
            jugadorEnZona = true;
            Debug.Log("Pulsa E para recoger el libro: " + gameObject.name); // Feedback para el usuario
        }
    }

    // Se llama cuando el Jugador sale de la zona del libro
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnZona = false;
        }
    }

    void RecogerLibro()
    {
        if (manager != null)
        {
            // 1. Reportar al Manager que se ha recogido un libro
            manager.LibroRecogido();
            
            // 2. Destruir el libro del escenario
            Destroy(gameObject);
        }
    }
}
