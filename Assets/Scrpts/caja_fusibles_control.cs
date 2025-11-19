using UnityEngine;
using UnityEngine.InputSystem;

public class caja_fusibles_control : MonoBehaviour
{
    
    private bool palanca_activa = false;
    private bool luzRoja_activa = true;
    private bool luzVerde_activa = false;

    private bool luz_av_act = false;

    public float speed = 2f; // velocidad de movimiento vertical

    //Palanca movimiento
    public float posXAbajo = 34f;   // posición inicial de la palanca (abajo)
    public float posXArriba = -20f; // posición cuando se abre (arriba)

    private Quaternion openRotation;

    [Header("Knob (perilla)")]
    public Transform knob;
    public Transform knob2;
    public Transform knob3;
    public float desplazamientoX = -0.097f; // cuánto se mueve a la derecha
    private Vector3 knobInicial;
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
        // Posición inicial
        openRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(posXArriba, 0, 0));

        mate_luzroja = luzRoja.GetComponent<Renderer>().material;
        mate_luzverde = luzVerde.GetComponent<Renderer>().material;
        mate_av = luz_av.GetComponent<Renderer>().material;

        // Luces
        ActivarEmision(mate_luzroja, Color.red, luzRoja_activa);
        ActivarEmision(mate_luzverde, Color.green, luzVerde_activa);
        ActivarEmision(mate_av, Color.yellow, luz_av_act);

        knobInicial = knob.position;
        knobObjetivo = knob.position + new Vector3(0, 0, desplazamientoX);
        knobObjetivo2 = knob2.position + new Vector3(0, 0, desplazamientoX);
        knobObjetivo3 = knob3.position + new Vector3(0, 0, desplazamientoX);
    }

    void Update()
    {
        // Detectar tecla P
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            palanca_activa = true;
            luzVerde_activa = true;
            luzRoja_activa = false;
            luz_av_act = true;

            ActivarEmision(mate_luzverde, Color.green, luzVerde_activa);
            ActivarEmision(mate_luzroja, Color.red, luzRoja_activa);
            ActivarEmision(mate_av, Color.yellow, luz_av_act);

        }

        // Mover palanca verticalmente
        if (palanca_activa)
        {
            transform.rotation = Quaternion.Lerp(openRotation, transform.rotation, Time.deltaTime * speed);
            knobInicial = Vector3.Lerp(knob.position, knobObjetivo, Time.deltaTime * speed);
            knob.position = knobInicial;

            knobInicial = Vector3.Lerp(knob2.position, knobObjetivo2, Time.deltaTime * speed);
            knob2.position = knobInicial;

            knobInicial = Vector3.Lerp(knob3.position, knobObjetivo3, Time.deltaTime * speed);
            knob3.position = knobInicial;
            
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
