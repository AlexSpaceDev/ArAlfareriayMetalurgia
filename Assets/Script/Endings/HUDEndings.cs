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
        int alfareria = GameData.tempScoreAlfareria;
        int metalurgia = GameData.tempScoreMetalurgia;

        switch (GameData.finalToShow)
        {
            case 1: // Final A
                scoreText.text =
                    GameData.finishedAlfareria ?
                    $"Puntaje: {alfareria}" :
                    $"Puntaje: {metalurgia}";
                break;

            case 3: // Final C
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
        GameData.tempScore = 0;
        GameData.tempScoreAlfareria = 0;
        GameData.tempScoreMetalurgia = 0;
        GameData.finishedAlfareria = false;
        GameData.finishedMetalurgia = false;
        GameData.finalToShow = 0;
        SceneManager.LoadScene("MainMenu");
    }
}
