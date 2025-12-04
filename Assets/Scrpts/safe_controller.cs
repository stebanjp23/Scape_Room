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
    private bool puzleResuelto = false;
     private Main_puzzles_sotano gestorPuzzles; 

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
        
        puzleResuelto = true;
        animando = true;
        cajaAbierta = true;
       

        if (gestorPuzzles != null)
        {
            gestorPuzzles.ReportarPuzleResuelto("Llave");
        }
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
