using UnityEngine;
using UnityEngine.SceneManagement;

public class GoARScene : MonoBehaviour
{
   public void GoAlfareria()
    {
        SceneManager.LoadScene("AlfareriaAR_1");
    }

    public void GoMetalurgia()
    {
        SceneManager.LoadScene("MetalurgiaAR_1");
    }
}
