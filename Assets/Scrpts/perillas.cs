using UnityEngine;

public class perillas : MonoBehaviour
{
     // Asigna el controlador principal (caja_fusibles_control) aquí
    public caja_fusibles_control controladorPrincipal; 

    // 1, 2, o 3 para identificar cuál perilla es (ASIGNAR EN EL INSPECTOR)
    public int idPerilla; 

    // Distancia de movimiento por clic (ASIGNAR EN EL INSPECTOR)
    public float movimientoPorClic = 0.03f; 

    // Esto se llama al hacer clic en el objeto (requiere un Collider)
    void OnMouseDown()
    {
        // Mueve la perilla una distancia en el eje Z local
        transform.localPosition += new Vector3(0, 0, movimientoPorClic);

        // Informa al controlador principal la nueva posición Z
        if (controladorPrincipal != null)
        {
            // Le pasamos la posición Z local (que es relativa al objeto padre)
            controladorPrincipal.VerificarAjuste(idPerilla, transform.localPosition.z);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
