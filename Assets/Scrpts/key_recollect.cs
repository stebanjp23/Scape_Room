using UnityEngine;
using UnityEngine.InputSystem;


public class key_recollect : MonoBehaviour
{
    private bool jugadorEnZona = false;

    void Update()
    {
        // Solo se puede recoger si el jugador está cerca y pulsa E
        if (jugadorEnZona && Keyboard.current.eKey.wasPressedThisFrame)
        {
            RecogerLlave();
        }
    }

    private void RecogerLlave()
    {
        Debug.Log("¡Llave recogida!");
        // Aquí puedes hacer lo que quieras: añadir al inventario, desbloquear puerta, etc.
    
        InventarioJugador.TieneLlave = true;

        Destroy(gameObject); // Destruye la llave para simular que la recoge
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnZona = true;
            Debug.Log("Pulsa E para recoger la llave.");
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
