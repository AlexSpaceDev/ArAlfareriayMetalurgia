using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsMenu : MonoBehaviour
{
    public void QuitGame()
    {
        // Sale de la app (solo funciona en build, no en editor)
        Application.Quit();
    }
}