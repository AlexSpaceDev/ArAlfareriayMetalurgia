using UnityEngine;

public class UnlockCollectibleButton : MonoBehaviour
{
    public GameData.CollectibleID collectibleToUnlock;

    public void Unlock()
    {
        GetCollect.Unlock(collectibleToUnlock);
    }
}
