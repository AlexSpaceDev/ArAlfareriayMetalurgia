using UnityEngine;
using UnityEngine.SceneManagement;

public class ProbarAnotherAR : MonoBehaviour
{
    public void ProbarMetalurgia()
    {
        GameData.directMetalurgia = true;
        SceneManager.LoadScene("Prologue");
    }
}
