using UnityEngine;

public class libros_mager : MonoBehaviour
{
    [Header("Configuración del Puzle")]
    public int totalLibrosRequeridos = 3; // ¡Cambia este valor en el Inspector!
    private int librosRecogidos = 0;
    private bool puzleResuelto = false;

    [Header("Mecanismo de Reacción (Ejemplo)")]
    public GameObject estanteriaMovil; // Estantería que se mueve o puerta secreta
    public float distanciaMovimiento = 2f; // Distancia para mover la estantería (eje Z local)
    public float velocidadMovimiento = 1f;
    private Vector3 posicionObjetivo;
    
    // Referencia al gestor central para reportar
    private Main_puzzles_sotano gestorPuzzles; 


    void Start()
    {
        gestorPuzzles = FindFirstObjectByType<Main_puzzles_sotano>();
        if (estanteriaMovil != null)
        {
            // Define la posición a la que se debe mover la estantería
            posicionObjetivo = estanteriaMovil.transform.localPosition + new Vector3(0, 0, distanciaMovimiento);
        }
    }

    public void LibroRecogido()
    {
        if (puzleResuelto) return;

        librosRecogidos++;
        Debug.Log("Libros recogidos: " + librosRecogidos + " / " + totalLibrosRequeridos);

        // Verificar si se ha alcanzado el total
        if (librosRecogidos >= totalLibrosRequeridos)
        {
            ResolverPuzle();
        }
    }

    void ResolverPuzle()
    {
        puzleResuelto = true;
        Debug.Log("¡Puzle de Libros Resuelto!");

        // Reportar al gestor central
        if (gestorPuzzles != null)
        {
            gestorPuzzles.ReportarPuzleResuelto("Libros"); 
        }
        
        // Dar feedback visual (activar movimiento)
        // Opcional: Sonido de mecanismo
    }
    
    void Update()
    {
        // Si el puzle está resuelto, animamos el mecanismo de reacción
        if (puzleResuelto && estanteriaMovil != null)
        {
            estanteriaMovil.transform.localPosition = Vector3.Lerp(
                estanteriaMovil.transform.localPosition, 
                posicionObjetivo, 
                Time.deltaTime * velocidadMovimiento
            );
        }
    }
}
