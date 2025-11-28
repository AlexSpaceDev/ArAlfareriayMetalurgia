using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalizarButton : MonoBehaviour
{
    public void Finalizar()
    {
        int alfareria = GameData.scoreAlfareria;
        int metalurgia = GameData.scoreMetalurgia;

        bool completedAlfareria = alfareria > 0;
        bool completedMetalurgia = metalurgia > 0;

        // ---------------------------------------------------------
        // SI YA DESBLOQUEÓ FINAL C → SIEMPRE MOSTRAR FINAL C
        // ---------------------------------------------------------
        if (GameData.finalCUnlocked)
        {
            GameData.finalAUnlocked = true; // redundante pero seguro
            GameData.finalCUnlocked = true;
            GameData.finalToShow = 3;

            SaveManager.SaveGame();
            SceneManager.LoadScene("Endings");
            return;
        }

        // ---------------------------------------------------------
        // PRIMERA VEZ FINAL C → SI COMPLETÓ AMBOS CAMINOS
        // ---------------------------------------------------------
        if (completedAlfareria && completedMetalurgia)
        {
            GameData.finalAUnlocked = true;
            GameData.finalCUnlocked = true;
            GameData.finalToShow = 3;

            SaveManager.SaveGame();
            SceneManager.LoadScene("Endings");
            return;
        }

        // ---------------------------------------------------------
        // FINAL A → SOLO COMPLETÓ UN CAMINO
        // ---------------------------------------------------------
        GameData.finalAUnlocked = true;
        GameData.finalToShow = 1;

        SaveManager.SaveGame();
        SceneManager.LoadScene("Endings");
    }
}
