using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int totalScore = 0;
    public int completedTargets = 0;

    public ImageTargetController[] targets;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // Activar solo el primer target
        ActivateTarget(0);
    }

    public void AddScore(int points)
    {
        totalScore += points;

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
            return;
        }

        // Activar el siguiente
        ActivateTarget(completedTargets);
    }

    private void ActivateTarget(int index)
    {
        for (int i = 0; i < targets.Length; i++)
        {
            targets[i].SetInteractable(i == index);
        }
    }
}

