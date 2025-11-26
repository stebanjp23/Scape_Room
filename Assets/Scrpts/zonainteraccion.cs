using UnityEngine;

public class zonainteraccion : MonoBehaviour
{
    [Header("Referencia Opcional")]
    // Si la cámara sigue rotando al liberar el mouse, asigna aquí 
    // el script de control de la cámara para deshabilitarlo.
    public MonoBehaviour scriptControlCamara;
    void OnTriggerEnter(Collider other)
    {
        // Asegúrate de que el objeto de tu jugador tenga la etiqueta "Player"
        if (other.CompareTag("Player"))
        {
            // 1. LIBERAR CURSOR
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None; 
            
            // 2. (OPCIONAL) Detener la rotación de la cámara
            if (scriptControlCamara != null)
            {
                scriptControlCamara.enabled = false;
            }
            
            Debug.Log("Cursor liberado: ¡Haz clic en las perillas!");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 1. BLOQUEAR CURSOR
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked; 
            
            // 2. (OPCIONAL) Reactivar la rotación de la cámara
            if (scriptControlCamara != null)
            {
                scriptControlCamara.enabled = true;
            }

            Debug.Log("Cursor bloqueado: Control de cámara activado.");
        }
    }
}
