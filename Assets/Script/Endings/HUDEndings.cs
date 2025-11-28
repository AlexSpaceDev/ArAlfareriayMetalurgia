using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class HUDEndings : MonoBehaviour
{
    public TextMeshProUGUI scoreText; 
    public Button quitButton;
    public Button goMenuButton;

    void Start()
    {
        UpdateScoreDisplay();

        quitButton.onClick.AddListener(QuitGame);
        goMenuButton.onClick.AddListener(GoToMenu);
    }

    private void UpdateScoreDisplay()
    {
        int alfareria = GameData.scoreAlfareria;
        int metalurgia = GameData.scoreMetalurgia;

        switch (GameData.finalToShow)
        {
            case 1: // Final A (solo completó un camino)
                scoreText.text = $"Puntaje: {(alfareria > 0 ? alfareria : metalurgia)}";
                break;

            case 3: // Final C (completó ambos)
                scoreText.text = $"Alfarería: {alfareria}   |   Metalurgia: {metalurgia}";
                break;

            default:
                scoreText.text = "";
                break;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
