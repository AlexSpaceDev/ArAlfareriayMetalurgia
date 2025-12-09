using UnityEngine;

public class ResetCollectiblesButton : MonoBehaviour
{
    public void ResetAll()
    {
        GameData.ResetCollectibles();
        SaveManager.ResetCollectiblesInSave();

        // Refrescar todos los CollectibleUI
        foreach (var ui in FindObjectsByType<CollectibleUI>(FindObjectsSortMode.None))
        {
            ui.SendMessage("Start"); // fuerza recargar estado visual
        }

    }
}
