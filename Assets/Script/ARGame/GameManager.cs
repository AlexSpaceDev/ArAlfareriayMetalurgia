using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum GameType { Alfareria, Metalurgia }
    public GameType currentGame;

    public int targetIndex; // este target es el 1, 2, 3, 4 o 5

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        // Si estamos en la primera escena del camino, reiniciar puntaje temporal
        if (sceneName.EndsWith("_1"))
        {
            GameData.tempScore = 0;
        }

        // Cargar el puntaje temporal actual
        HUDController.Instance.UpdateScore(GameData.tempScore);
        
    }

    // Agregar puntaje temporal
    public void AddScore(int points)
    {
        GameData.tempScore += points;

        HUDController.Instance.UpdateScore(GameData.tempScore);
        HUDController.Instance.ShowFeedback(points);
    }

    public void OnTargetCompleted()
    {
        // Si completó todos los targets
        if (targetIndex < 5)
        {
            // Cargar siguiente escena
            string nextScene =
                (currentGame == GameType.Alfareria)
                ? "AlfareriaAR_" + (targetIndex + 1)
                : "MetalurgiaAR_" + (targetIndex + 1);

            SceneManager.LoadScene(nextScene);
        }
        else
        {
            // Última escena: guardar el puntaje temporal del camino
            if (currentGame == GameType.Alfareria)
            {
                GameData.tempScoreAlfareria = GameData.tempScore;
                GameData.finishedAlfareria = true;
            }
            else
            {
                GameData.tempScoreMetalurgia = GameData.tempScore;
                GameData.finishedMetalurgia = true;
            }

            string scoreScene =
                currentGame == GameType.Alfareria ?
                "AlfareriaScore" : "MetalurgiaScore";

            SceneManager.LoadScene(scoreScene);               
        }
    }
}

