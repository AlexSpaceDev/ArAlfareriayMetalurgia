using UnityEngine;
using UnityEngine.SceneManagement;

public class GoARScene : MonoBehaviour
{             
    public void GoAlfareria()
    {
        // Si NO vienes desde Metalurgia, significa que es una run nueva
        if (!GameData.directMetalurgia && !GameData.directAlfareria)
        {
            GameData.StartNewRun();
            GameData.tempCompletedAlfareria = false;
            GameData.tempCompletedMetalurgia = false;
        }
        
        SceneManager.LoadScene("AlfareriaAR_1");
        
    }

    public void GoMetalurgia()
    {
        // Si NO vienes desde Alfarería, significa que es una run nueva
        if (!GameData.directAlfareria && !GameData.directMetalurgia)
        {
            GameData.StartNewRun();
            GameData.tempCompletedAlfareria = false;
            GameData.tempCompletedMetalurgia = false;
        }

        SceneManager.LoadScene("MetalurgiaAR_1");
    }

}
