using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsMenu : MonoBehaviour
{
    public void Achievments()
    {
        // Carga la escena de logros
        Debug.Log("Cargando los logros...");
    }

    public void QuitGame()
    {
        // Sale de la app (solo funciona en build, no en editor)
        Application.Quit();
        Debug.Log("Saliendo del juego...");
    }
}