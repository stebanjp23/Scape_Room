using UnityEngine;

public class libros_mager : MonoBehaviour
{
    [Header("Configuración del Puzle")]
    public int totalLibrosRequeridos = 3; // ¡Cambia este valor en el Inspector!
    private int librosRecogidos = 0;
    private bool puzleResuelto = false;
    
    // Referencia al gestor central para reportar
    private Main_puzzles_sotano gestorPuzzles; 

    [Header("Recompensa por Puzle")]
    public safe_controller cajaRecompensa; // Referencia al script de la caja


    void Start()
    {
        gestorPuzzles = FindFirstObjectByType<Main_puzzles_sotano>();
    }

    public void LibroRecogido()
    {

        if (puzleResuelto) return;

        librosRecogidos++;
        FindObjectOfType<MensajesGuia>().MostrarMensaje("Libros recogidos: " + librosRecogidos + " / " + totalLibrosRequeridos, 3f);

        // Verificar si se ha alcanzado el total
        if (librosRecogidos >= totalLibrosRequeridos)
        {
            ResolverPuzle();
        }
    }

    void ResolverPuzle()
    {
        puzleResuelto = true;

        // Reportar al gestor central
        if (gestorPuzzles != null)
        {
            gestorPuzzles.ReportarPuzleResuelto("Libros"); 
        }

        if (cajaRecompensa != null)
        {
            cajaRecompensa.ActivarAperturaAutomatica();
        }
    }
    
    void Update()
    {

    }
}
