using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
 public string escenaJuego = "Habitación"; // Nombre de la escena con tus puzles

    public void Play()
    {
        SceneManager.LoadScene(escenaJuego);
    }

    public void Salir()
    {
        Application.Quit();
    }
}
