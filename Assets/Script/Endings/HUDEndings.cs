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
            case 1: // Final A
                scoreText.text = $"Puntaje: {alfareria}";
                break;

            case 2: // Final B
                scoreText.text = $"Puntaje: {metalurgia}";
                break;

            case 3: // Final C
                scoreText.text = $"Alfarería: {alfareria}   |   Metalurgia: {metalurgia}";
                break;

            case 4: // Final D
                scoreText.text = "20 PUNTOS";
                break;

            default:
                scoreText.text = "";
                break;
        }
    }

    private void QuitGame()
    {
        Application.Quit();
    }

    private void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
