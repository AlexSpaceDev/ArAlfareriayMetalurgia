using UnityEngine;
using Vuforia;

public class ARStarter : MonoBehaviour
{
    void Start()
    {
        VuforiaApplication.Instance.Initialize();

        if (GameData.directAlfareria)
        {
            GameData.directAlfareria = false;
        }

        if (GameData.directMetalurgia)
        {
            GameData.directMetalurgia = false;
        }
    }
}