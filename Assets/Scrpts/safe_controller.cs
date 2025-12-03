using UnityEngine;

public class safe_controller : MonoBehaviour
{
    [Header("Mecanismo")]
    public GameObject tapaDeCaja; // Objeto que rotará
    public GameObject llaveObjeto; // La llave que se va a revelar
    public float velocidadApertura = 2f; // Velocidad de rotación

    private bool cajaAbierta = false;
    private bool animando = false;

    private Quaternion rotInicial;
    private Quaternion rotObjetivo;

    void Start()
    {
        if (tapaDeCaja != null)
        {
            rotInicial = tapaDeCaja.transform.localRotation;
            rotObjetivo = rotInicial * Quaternion.Euler(0, 120f, 0); // Rotación de 120° en Y
        }

        if (llaveObjeto != null)
        {
            llaveObjeto.SetActive(false);
        }
    }

    public void ActivarAperturaAutomatica()
    {
        if (cajaAbierta) return;
        animando = true;
        cajaAbierta = true;
        Debug.Log("¡CAJA ABIERTA POR PUZZLE RESUELTO!");
    }

    void Update()
    {
        if (animando && tapaDeCaja != null)
        {
            tapaDeCaja.transform.localRotation = Quaternion.Lerp(
                tapaDeCaja.transform.localRotation, 
                rotObjetivo, 
                Time.deltaTime * velocidadApertura
            );

            // Verifica si la rotación está casi completa
            if (Quaternion.Angle(tapaDeCaja.transform.localRotation, rotObjetivo) < 0.1f)
            {
                tapaDeCaja.transform.localRotation = rotObjetivo;
                animando = false;

                // Revela la llave al terminar la rotación
                if (llaveObjeto != null)
                {
                    llaveObjeto.SetActive(true);
                }
            }
        }
    }
}
