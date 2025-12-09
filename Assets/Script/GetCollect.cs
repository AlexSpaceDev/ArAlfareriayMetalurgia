using UnityEngine;

public class GetCollect : MonoBehaviour
{
    public GameData.CollectibleID collectibleToUnlock;

    void Start()
    {
        Unlock(collectibleToUnlock);
    }

    public static void Unlock(GameData.CollectibleID id)
    {
        int index = (int)id;

        // Si ya estaba desbloqueado NO mostramos mensaje
        if (GameData.collectibleUnlocked[index])
            return;

        GameData.collectibleUnlocked[index] = true;

        // Mostrar mensaje de UI
        if (ToastMessage.Instance != null)
        {
            ToastMessage.Instance.Show("¡Obtuviste un coleccionable!");
        }

        SaveManager.SaveGame();
    }
}
