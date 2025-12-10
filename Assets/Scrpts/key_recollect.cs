using UnityEngine;
using UnityEngine.InputSystem;


public class key_recollect : MonoBehaviour
{
    private bool jugadorEnZona = false;
    private Main_puzzles_sotano gestorPuzzles; 

    
    void Start()
    {
        gestorPuzzles = FindFirstObjectByType<Main_puzzles_sotano>();
    }

    void Update()
    {
        // Solo se puede recoger si el jugador está cerca y pulsa E
        if (jugadorEnZona && Keyboard.current.eKey.wasPressedThisFrame)
        {
            RecogerLlave();
            gestorPuzzles.ReportarPuzleResuelto("Llave"); 
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
