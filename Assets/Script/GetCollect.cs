using UnityEngine;

public class GetCollect : MonoBehaviour
{
    void Start()
    {
        GameData.collectibleCanutoUnlocked = true;
    SaveManager.SaveGame();

    }
}
