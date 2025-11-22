using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalizarButton : MonoBehaviour
{
    public void Finalizar()
    {
        string current = SceneManager.GetActiveScene().name;

        int alfareria = GameData.scoreAlfareria;
        int metalurgia = GameData.scoreMetalurgia;

        bool passedAlfareria = alfareria >= 0;    // Puede ser 0 puntos, igual cuenta
        bool passedMetalurgia = metalurgia >= 0;  // Igual

        // ---- LÓGICA DE FINALES ----

        // ----------------------------------------------------
        // FINAL D (Perfecto en ambos)
        // ----------------------------------------------------
        if (passedAlfareria && passedMetalurgia && alfareria == 10 && metalurgia == 10)
        {
            GameData.finalAUnlocked = true;
            GameData.finalBUnlocked = true;
            GameData.finalCUnlocked = true;
            GameData.finalDUnlocked = true;

            GameData.finalToShow = 4;
            SceneManager.LoadScene("Endings");
            return;
        }

        // ----------------------------------------------------
        // FINAL A (Solo pasó Alfarería)
        // ----------------------------------------------------
        if (current == "AlfareriaScore")
        {
            if (!GameData.finalBUnlocked && !GameData.finalCUnlocked)
            {
                GameData.finalAUnlocked = true;
                GameData.finalToShow = 1;
                SceneManager.LoadScene("Endings");
                return;
            }
        }

        // ----------------------------------------------------
        // FINAL B (Solo pasó Metalurgia)
        // ----------------------------------------------------
        if (current == "MetalurgiaScore")
        {
            if (!GameData.finalAUnlocked && !GameData.finalCUnlocked)
            {
                GameData.finalBUnlocked = true;
                GameData.finalToShow = 2;
                SceneManager.LoadScene("Endings");
                return;
            }
        }

        // ----------------------------------------------------
        // FINAL C (Pasó ambos, pero no perfecto)
        // ----------------------------------------------------
        GameData.finalAUnlocked = true;
        GameData.finalBUnlocked = true;
        GameData.finalCUnlocked = true;
        
        GameData.finalToShow = 3;
        SceneManager.LoadScene("Endings");
    }
}

