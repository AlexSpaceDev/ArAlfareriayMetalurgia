using UnityEngine;
using Vuforia;

public class ARStarter : MonoBehaviour
{
    void Start()
    {
        VuforiaApplication.Instance.Initialize();
    }
}