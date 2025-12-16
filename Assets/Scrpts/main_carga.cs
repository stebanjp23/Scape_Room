using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using TMPro; // Agrega esta línea si usas TextMeshPro
using System.Collections;

public class main_carga : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
public string escenaJuego = "Habitación";
public Slider progressBar; // Arrastra aquí el Slider
public TMP_Text progreso;
    void Start()
    {
        StartCoroutine(LoadAsyncOperation());
    }

    public IEnumerator LoadAsyncOperation()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(escenaJuego);
        operation.allowSceneActivation = false;

         float displayedProgress = 0f;

        while (!operation.isDone)
        {
            // Progreso real (0 a 0.9)
            float targetProgress = Mathf.Clamp01(operation.progress / 0.9f);

            // Hacer la barra más suave y visible
            displayedProgress = Mathf.MoveTowards(displayedProgress, targetProgress, Time.deltaTime);
            
            if (progressBar != null)
                progressBar.value = displayedProgress;

            if (progreso != null)
                progreso.text = Mathf.RoundToInt(displayedProgress * 100f) + "%";

            // Cuando llega a 0.9, esperar un momento antes de activar la escena
            if (operation.progress >= 0.9f && displayedProgress >= 1f)
            {
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
