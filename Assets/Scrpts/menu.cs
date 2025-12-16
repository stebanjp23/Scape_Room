using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class menu : MonoBehaviour
{
 public string carga = "Pantalla_carga"; // Nombre de la escena con tus puzles

    
    public void Play()
    {
        SceneManager.LoadScene(carga);
    }


    public void Salir()
    {
        Application.Quit();
    }
    
}
