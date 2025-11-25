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
        // Cargar puntaje según la escena
        int score = (currentGame == GameType.Alfareria) 
                    ? GameData.scoreAlfareria 
                    : GameData.scoreMetalurgia;

        HUDController.Instance.UpdateScore(score);
        
    }

    public void AddScore(int points)
    {
        // Guardar puntaje en GameData según el tipo
        if (currentGame == GameType.Alfareria)
            GameData.scoreAlfareria += points;
        else 
            GameData.scoreMetalurgia += points;

        HUDController.Instance.UpdateScore(
            currentGame == GameType.Alfareria ?
            GameData.scoreAlfareria : GameData.scoreMetalurgia
        );

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
            // Última escena
            string scoreScene =
                currentGame == GameType.Alfareria ?
                "AlfareriaScore" : "MetalurgiaScore";

            SceneManager.LoadScene(scoreScene);               
        }
    }

}

