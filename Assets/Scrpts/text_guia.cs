using UnityEngine;

using UnityEngine;
using TMPro;


public class MensajesGuia : MonoBehaviour
{
    public TextMeshProUGUI textoMensaje;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         textoMensaje.text = "Tienes que arreglar la luz"; // Vacío al inicio
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MostrarMensaje(string mensaje, float duracion = 3f)
    {
        StopAllCoroutines();
        StartCoroutine(MostrarPorTiempo(mensaje, duracion));
    }

    private System.Collections.IEnumerator MostrarPorTiempo(string mensaje, float duracion)
    {
        textoMensaje.text = mensaje;
        yield return new WaitForSeconds(duracion);
        textoMensaje.text = "";
    }
}
