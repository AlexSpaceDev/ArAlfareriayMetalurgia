using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum GameType { Alfareria, Metalurgia }
    public GameType currentGame;

    public int totalScore = 0;
    public int completedTargets = 0;

    public ImageTargetController[] targets;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // Cargar puntaje según la escena
        if (currentGame == GameType.Alfareria)
            totalScore = GameData.scoreAlfareria;
        else 
            totalScore = GameData.scoreMetalurgia;

        HUDController.Instance.UpdateScore(totalScore);
        
        // Activar solo el primer target
        ActivateTarget(0);
    }

    public void AddScore(int points)
    {
        totalScore += points;

        // Guardar puntaje en GameData según el tipo
        if (currentGame == GameType.Alfareria)
            GameData.scoreAlfareria = totalScore;
        else 
            GameData.scoreMetalurgia = totalScore;

        HUDController.Instance.UpdateScore(totalScore);
        HUDController.Instance.ShowFeedback(points);
    }

    public void OnTargetCompleted()
    {
        completedTargets++;

        // Solo mostrar mensaje SI NO es el último target
        if (completedTargets < targets.Length)
        {
            HUDController.Instance.ShowNextImageMessage();
        }

        // Si completó todos los targets
        if (completedTargets == targets.Length)
        {
            Debug.Log("Logrado! Puntaje final: " + totalScore);

            // Guardar puntaje final (ya lo hacemos en AddScore, pero por si acaso)
            if (currentGame == GameType.Alfareria)
            GameData.scoreAlfareria = totalScore;
            else
            GameData.scoreMetalurgia = totalScore;

            // Cambiar de escena después de 2 segundos
            StartCoroutine(GoToScoreScene());

            return;
        }

        // Activar el siguiente
        ActivateTarget(completedTargets);
    }

    private IEnumerator GoToScoreScene()
    {
        yield return new WaitForSeconds(2f); // Esperar 2 segundos

        if (currentGame ==  GameType.Alfareria)
            SceneManager.LoadScene("AlfareriaScore");
        else
            SceneManager.LoadScene("MetalurgiaScore");
    }

    private void ActivateTarget(int index)
    {
        for (int i = 0; i < targets.Length; i++)
        {
            targets[i].SetInteractable(i == index);
        }
    }
}

