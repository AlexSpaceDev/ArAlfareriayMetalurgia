using UnityEngine;
using UnityEngine.SceneManagement;

public class ProbarAnotherAR : MonoBehaviour
{
    public void ProbarMetalurgia()
    {
        GameData.directMetalurgia = true;
        SaveManager.SaveGame();
        SceneManager.LoadScene("Prologue");
    }

    public void ProbarAlfareria()
    {
        GameData.directAlfareria = true;
        SaveManager.SaveGame();
        SceneManager.LoadScene("Prologue");
    }
}
