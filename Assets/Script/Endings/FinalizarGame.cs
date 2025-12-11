using UnityEngine;
using UnityEngine.SceneManagement;


    public class FinalizarButton : MonoBehaviour
    {
        public void Finalizar()
        {
            
            // Si no se está jugando una partida no se debería mostrar final
            if (!GameData.isPlaying)
                return;

            bool alfareria = GameData.tempCompletedAlfareria;
            bool metalurgia = GameData.tempCompletedMetalurgia;

            // -------------------------------
            // FINAL C → completó ambos caminos
            // -------------------------------
            if (alfareria && metalurgia)
            {
                GameData.finalCUnlocked = true; // Esto es global
                GameData.finalToShow = 3;

                EndRun();
                return;
            }

            // -------------------------------
            // FINAL A → solo completó un camino
            // -------------------------------
            if (alfareria || metalurgia)
            {
                GameData.finalAUnlocked = true;
                GameData.finalToShow = 1;

                EndRun();
                return;
            }

            // (Muy raro: no terminó nada)
            GameData.finalToShow = 0;
            EndRun();
            
        }

        void EndRun()
        {
            GameData.isPlaying = false;
            GameData.directAlfareria = false;
            GameData.directMetalurgia = false;

            SaveManager.SaveGame();
            SceneManager.LoadScene("Endings");
        }
    }

