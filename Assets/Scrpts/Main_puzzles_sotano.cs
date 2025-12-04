using UnityEngine;

public class Main_puzzles_sotano : MonoBehaviour
{
    // --- ESTADO DE LOS PUZLES ---
    private bool puzleLibrosCompleto = false;
    private bool puzleFuciblesCompleto = false; // Usado para tu Puzle 2
    private bool puzle3Completo = false;        // Renombrado para consistencia
    public GameObject puertaFinal; // Arrastra aquí la puerta final en el Inspector


    // Contador para mostrar al jugador
    private int puzlesResueltos = 0;
    private const int TOTAL_PUZZLES = 3;

    void Start()
    {
        // La rotación inicial es la rotación actual del objeto (la puerta)
        closedRotation = transform.rotation;
        // La rotación final es la rotación inicial más el ángulo de apertura
        openRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, openAngle, 0));
    }

    // --- FUNCIÓN PÚBLICA PARA REPORTAR ESTADO ---
    public void ReportarPuzleResuelto(string nombrePuzle)
    {
        bool resueltoAhora = false;
        
        if(nombrePuzle == "Fucibles" && !puzleFuciblesCompleto)
        {
            puzleFuciblesCompleto = true;
            resueltoAhora = true;
            Object.FindFirstObjectByType<MensajesGuia>().MostrarMensaje("Debes buscar el mensaje secreto", 3f);
        }
        else if  (nombrePuzle == "Libros" && !puzleLibrosCompleto)
        {
            puzleLibrosCompleto = true;
            resueltoAhora = true;
            Object.FindFirstObjectByType<MensajesGuia>().MostrarMensaje("Busca la llave", 3f);
        }
        else if (nombrePuzle == "Llave" && !puzle3Completo) 
        {
            puzle3Completo = true;
            resueltoAhora = true;
        }

        if (resueltoAhora)
        {
            puzlesResueltos++; // Incrementa el contador global
            Debug.Log($"Has Resuelto {puzlesResueltos} puzles de {TOTAL_PUZZLES}");
        }

        // Después de cada reporte, verifica si se cumplen las condiciones finales
        VerificarCondicionVictoria();
    }

    // --- FUNCIÓN PARA VERIFICAR LA CONDICIÓN FINAL ---
    private void VerificarCondicionVictoria()
    {
        if (puzleLibrosCompleto && puzleFuciblesCompleto && puzle3Completo)
        {
            puertaFinal.GetComponent<door_controller>().llave = true;
        }
    }

    void Update()
    {
    }
}