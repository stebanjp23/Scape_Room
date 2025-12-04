using UnityEngine;
using UnityEngine.InputSystem;
using System; // Agregado si usas FindFirstObjectByType en versiones modernas

public class caja_fusibles_control : MonoBehaviour
{
    public GameObject luzTechoObjeto; // Arrastra la luz (o el foco) de techo aquí
    private Light luzTechoComponente; // Para controlar la intensidad

    // --- GESTIÓN DE PUZLE CENTRAL ---
    private bool puzleResuelto = false; 
    private Main_puzzles_sotano gestorPuzzles; 
  
    // --- ESTADO VISUAL ---
    private bool palanca_activa = false;
    private bool luzRoja_activa = true;
    private bool luzVerde_activa = false;


    private bool luz_av_act = false;

    public float speed = 2f; // velocidad de movimiento vertical
    private bool jugadorEnZona = false;
    //Palanca movimiento
    public float posXAbajo = 34f;   
    public float posXArriba = -20f; 
    private Quaternion openRotation;
    private Quaternion closedRotation; 

    [Header("Knob (fusibles)")]
    public Transform knob;
    public Transform knob2;
    public Transform knob3;
    // ¡CORREGIDO! Usamos X para el desplazamiento lateral.
    public float desplazamientoX = -0.097f; 
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
        // Usar FindFirstObjectByType para evitar el warning
        gestorPuzzles = FindFirstObjectByType<Main_puzzles_sotano>(); 
        if (gestorPuzzles == null)
        {
            Debug.LogError("Main_puzzles_sotano no encontrado. Asegúrate de que está en la escena.");
        }

        if (luzTechoObjeto != null)
        {
            // OBTENER EL COMPONENTE LIGHT
            luzTechoComponente = luzTechoObjeto.GetComponent<Light>(); 
            if (luzTechoComponente != null)
            {
                // Apaga la luz al inicio
                luzTechoComponente.enabled = false; 
            }
        }

        // Posiciones de rotación
        closedRotation = transform.rotation;
        openRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(posXArriba, 0, 0));


        mate_luzroja = luzRoja.GetComponent<Renderer>().material;
        mate_luzverde = luzVerde.GetComponent<Renderer>().material;
        mate_av = luz_av.GetComponent<Renderer>().material;

        // Luces iniciales
        ActivarEmision(mate_luzroja, Color.red, luzRoja_activa);
        ActivarEmision(mate_luzverde, Color.green, luzVerde_activa);
        ActivarEmision(mate_av, Color.yellow, luz_av_act);

        // ¡CORRECCIÓN CLAVE! Sumamos el desplazamiento a la coordenada X local
        knobObjetivo = knob.localPosition + new Vector3(desplazamientoX, 0, 0);
        knobObjetivo2 = knob2.localPosition + new Vector3(desplazamientoX, 0, 0);
        knobObjetivo3 = knob3.localPosition + new Vector3(desplazamientoX, 0, 0);
    }


    void Update()
    {
        // 1. DETECCIÓN DE TECLA e

        if (Keyboard.current.eKey.wasPressedThisFrame && !puzleResuelto && jugadorEnZona)
        {
            // Activación y luces
            palanca_activa = true;
            luzVerde_activa = true;
            luzRoja_activa = false;
            luz_av_act = true;

            ActivarEmision(mate_luzverde, Color.green, luzVerde_activa);
            ActivarEmision(mate_luzroja, Color.red, luzRoja_activa);
            ActivarEmision(mate_av, Color.yellow, luz_av_act);

            // Reporte de victoria
            if (gestorPuzzles != null)
            {
                gestorPuzzles.ReportarPuzleResuelto("Fucibles"); 
                puzleResuelto = true; 

                if (luzTechoComponente != null) 
                {
                    luzTechoComponente.enabled = true; 
                }
            }
        }


        // 2. ANIMACIÓN DE LA PALANCA Y LOS FUSIBLES
        if (palanca_activa)
        {
            // Mover la palanca (rotación)
            transform.rotation = Quaternion.Lerp(transform.rotation, openRotation, Time.deltaTime * speed);
            
            // Mover los fusibles (knobs) en el eje X
            knob.localPosition = Vector3.Lerp(knob.localPosition, knobObjetivo, Time.deltaTime * speed);
            knob2.localPosition = Vector3.Lerp(knob2.localPosition, knobObjetivo2, Time.deltaTime * speed);
            knob3.localPosition = Vector3.Lerp(knob3.localPosition, knobObjetivo3, Time.deltaTime * speed);

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        // Comprueba si el objeto que entró es el Jugador (debe tener la etiqueta "Player")
        if (other.CompareTag("Player"))
        {
            jugadorEnZona = true;
            FindObjectOfType<MensajesGuia>().MostrarMensaje("Pulsa E para reiniciar el sistema", 3f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnZona = false;
        }
    }

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