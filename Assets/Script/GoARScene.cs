using UnityEngine;
using UnityEngine.SceneManagement;

public class GoARScene : MonoBehaviour
{
   public void GoAlfareria()
    {
        SceneManager.LoadScene("AlfareriaAR");
    }

    public void GoMetalurgia()
    {
        SceneManager.LoadScene("MetalurgiaAR_1");
    }
}
