using UnityEngine;

public class BootLoader : MonoBehaviour
{
    void Awake()
    {
        SaveManager.LoadGame();
    }
}
