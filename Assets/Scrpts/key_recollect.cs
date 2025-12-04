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
        
        InventarioJugador.TieneLlave = true;

        Destroy(gameObject); // Destruye la llave para simular que la recoge
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnZona = true;
            FindObjectOfType<MensajesGuia>().MostrarMensaje("Pulsa E para recojer la llave", 3f);
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
