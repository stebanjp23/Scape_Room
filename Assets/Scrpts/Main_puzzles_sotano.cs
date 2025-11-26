using UnityEngine;

public class Main_puzzles_sotano : MonoBehaviour
{
    // --- ESTADO DE LOS PUZLES ---
    private bool puzleLibrosCompleto = false;
    private bool puzleFuciblesCompleto = false; // Usado para tu Puzle 2
    private bool puzle3Completo = false;        // Renombrado para consistencia

    // --- VARIABLES DE LA PUERTA (Ajustables en el Inspector) ---
    public float openAngle = -90f;              // Cuánto se abre la puerta (ej. -90 grados)
    public float speed = 2f;                    // Velocidad de apertura
    
    // --- LÓGICA DE APERTURA ---
    private bool isOpen = false;                // Estado que activa la rotación
    private Quaternion closedRotation;          // Rotación inicial
    private Quaternion openRotation;            // Rotación final
    
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
        
        if (nombrePuzle == "Libros" && !puzleLibrosCompleto)
        {
            puzleLibrosCompleto = true;
            resueltoAhora = true;
        }
        else if (nombrePuzle == "Fucibles" && !puzleFuciblesCompleto)
        {
            puzleFuciblesCompleto = true;
            resueltoAhora = true;
        }
        else if (nombrePuzle == "puzle3" && !puzle3Completo) // Corregido: usé puzle3Completo
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
        // CORRECCIÓN CLAVE: Verifica las 3 variables bool declaradas
        if (puzleLibrosCompleto && puzleFuciblesCompleto && puzle3Completo)
        {
            AbrirPuertaFinal();
        }
    }

    // --- FUNCIÓN PARA ACTIVAR LA APERTURA (SE LLAMA UNA SOLA VEZ) ---
    private void AbrirPuertaFinal()
    {
        Debug.Log("¡TODOS LOS PUZLES RESUELTOS! Abriendo puerta...");
        // Esto solo cambia el estado. La animación ocurre en Update()
        isOpen = true;
    }

    // --- FUNCIÓN DE ACTUALIZACIÓN (MANEJA LA ANIMACIÓN) ---
    void Update()
    {
        if (isOpen)
        {
            // Mueve la rotación de la puerta (transform.rotation)
            // de forma suave (Lerp) hacia la rotación final (openRotation)
            transform.rotation = Quaternion.Lerp(transform.rotation, openRotation, Time.deltaTime * speed);
        }
    }
}