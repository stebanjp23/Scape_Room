using UnityEngine;
using UnityEngine.InputSystem;

public class caja_fusibles_control : MonoBehaviour
{
    // --- GESTIÓN DE PUZLE CENTRAL ---
    private bool puzleResuelto = false; // Bandera para reportar una sola vez
    private Main_puzzles_sotano gestorPuzzles; // Referencia al Manager
    
    // --- LÓGICA DE PUZLE: PERILLAS ---
    private bool knob1Correcto = false;
    private bool knob2Correcto = false;
    private bool knob3Correcto = false;
    private bool perillasAjustadas = false; // Indica si las 3 están bien
    
    [Header("Lógica de Solución")]
    // Define el valor Z correcto para la solución (AJUSTAR EN EL INSPECTOR)
    public float limiteSolucionZ = 0.09f; 
    
    // --- ESTADO VISUAL ---
    private bool palanca_activa = false;
    private bool luzRoja_activa = true;
    private bool luzVerde_activa = false;

    private bool luz_av_act = false;

    public float speed = 2f; // velocidad de movimiento
    
    // --- MOVIMIENTO DE PALANCA ---
    public float posXAbajo = 34f;   // posición inicial de la palanca (abajo)
    
    public float posXArriba = -20f; // posición cuando se abre (arriba)

    private Quaternion openRotation;

    [Header("Knob (perilla)")]
    public Transform knob;
    public Transform knob2;
    public Transform knob3;
    public float desplazamientoX = -0.097f; // CUÁNTO se mueve la perilla (NO USADO CON CLIC)
    private Vector3 knobObjetivo, knobObjetivo2, knobObjetivo3;

    //Luces
    public GameObject luzVerde;
    public GameObject luzRoja;

    public GameObject luz_av;

    private Material mate_luzroja;
    private Material mate_luzverde;

    private Material mate_av;


    void Start()
    {
        gestorPuzzles = FindObjectOfType<Main_puzzles_sotano>();
        
        // Posición de rotación final de la palanca
        openRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(posXArriba, 0, 0));

        // Inicialización de materiales (requiere que los GameObjects tengan el componente Renderer)
        mate_luzroja = luzRoja.GetComponent<Renderer>().material;
        mate_luzverde = luzVerde.GetComponent<Renderer>().material;
        mate_av = luz_av.GetComponent<Renderer>().material;

        // Luces iniciales
        ActivarEmision(mate_luzroja, Color.red, luzRoja_activa);
        ActivarEmision(mate_luzverde, Color.green, luzVerde_activa);
        ActivarEmision(mate_av, Color.yellow, luz_av_act);

        // Se inicializan los objetivos de movimiento para las perillas (aunque la lógica de movimiento está en PerillaInteractiva)
        knobObjetivo = knob.position + new Vector3(0, 0, desplazamientoX);
        knobObjetivo2 = knob2.position + new Vector3(0, 0, desplazamientoX);
        knobObjetivo3 = knob3.position + new Vector3(0, 0, desplazamientoX);
    }

    // FUNCIÓN PÚBLICA LLAMADA POR PerillaInteractiva.cs
    public void VerificarAjuste(int idPerilla, float posicionActualZ)
    {
        // Compara la posición Z actual con el límite de la solución (usamos valor absoluto)
        bool estaCorrecto = (Mathf.Abs(posicionActualZ) >= limiteSolucionZ); 
        
        // Actualiza la bandera de la perilla específica
        if (idPerilla == 1)
            knob1Correcto = estaCorrecto;
        else if (idPerilla == 2)
            knob2Correcto = estaCorrecto;
        else if (idPerilla == 3)
            knob3Correcto = estaCorrecto;

        // Comprueba si TODAS están correctas
        perillasAjustadas = knob1Correcto && knob2Correcto && knob3Correcto;
        
        if (perillasAjustadas)
        {
            Debug.Log("¡PERILLAS AJUSTADAS! Palanca lista para activar (Tecla P).");
        }
    }

    void Update()
    {
        // Detectar tecla P
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            // CONDICIÓN CLAVE: Solo se puede activar si las perillas están ajustadas Y no se ha resuelto ya
            if (perillasAjustadas && !puzleResuelto)
            {
                // 1. ACTIVACIÓN VISUAL Y DE ESTADO
                palanca_activa = true;
                luzVerde_activa = true;
                luzRoja_activa = false;
                luz_av_act = true;

                ActivarEmision(mate_luzverde, Color.green, luzVerde_activa);
                ActivarEmision(mate_luzroja, Color.red, luzRoja_activa);
                ActivarEmision(mate_av, Color.yellow, luz_av_act);

                // 2. REPORTE AL GESTOR CENTRAL
                if (gestorPuzzles != null)
                {
                    gestorPuzzles.ReportarPuzleResuelto("Fucibles"); 
                    puzleResuelto = true; 
                }
            }
            else if (!perillasAjustadas)
            {
                 Debug.Log("ERROR: Las perillas no están en la posición correcta.");
                 // Feedback de error al jugador
            }
        } // Fin del if (Keyboard.current.pKey.wasPressedThisFrame)

        // Mover palanca verticalmente (si está activa)
        if (palanca_activa)
        {
            // Movimiento de la palanca (rotación)
            transform.rotation = Quaternion.Lerp(transform.rotation, openRotation, Time.deltaTime * speed);
            
            // Movimiento de los knobs (aunque su posición inicial fue modificada por el clic)
            Vector3 knobInicial = Vector3.Lerp(knob.position, knobObjetivo, Time.deltaTime * speed);
            knob.position = knobInicial;

            Vector3 knobInicial2 = Vector3.Lerp(knob2.position, knobObjetivo2, Time.deltaTime * speed);
            knob2.position = knobInicial2;

            Vector3 knobInicial3 = Vector3.Lerp(knob3.position, knobObjetivo3, Time.deltaTime * speed);
            knob3.position = knobInicial3;
        }
    }

     // Función para manejar la emisión de materiales PBR de las luces
     void ActivarEmision(Material mat, Color color, bool activo)
    {
        if (activo)
        {
            mat.EnableKeyword("_EMISSION");
            mat.SetColor("_EmissionColor", color * 2f); // brillo
        }
        else
        {
            mat.DisableKeyword("_EMISSION");
            mat.SetColor("_EmissionColor", Color.black);
        }
    }
}