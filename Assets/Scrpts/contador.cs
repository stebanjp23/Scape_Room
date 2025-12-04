using UnityEngine;
using TMPro;


public class contador : MonoBehaviour
{
    public TextMeshProUGUI countText;

    private float tiempo = 0f;
    private bool activo = true;

    // Update is called once per frame
    void Update()
    {
        if (!activo) return;

        tiempo += Time.deltaTime;

        int minutos = (int)(tiempo / 60);
        int segundos = (int)(tiempo % 60);

        countText.text = "Tiempo: "+minutos + ":" + (segundos < 10 ? "0" : "") + segundos;
    }

    public void DetenerContador()
    {
        activo = false;
    }
}
