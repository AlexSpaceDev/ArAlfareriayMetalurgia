using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class HUDController : MonoBehaviour
{
    public static HUDController Instance;

    [Header("UI References")]
    public TextMeshProUGUI scoreText;
    public CanvasGroup feedbackGroup;  // Para animar aparición
    public TextMeshProUGUI feedbackText;
    public TextMeshProUGUI nextImageText;

    private void Awake()
    {
        Instance = this;

        // Ocultar grupo de feedback al inicio
        feedbackGroup.alpha = 0f;
        nextImageText.gameObject.SetActive(false);
    }

    public void UpdateScore(int score)
    {
        scoreText.text = "Puntos: " + score;
    }

    public void ShowFeedback(int points)
    {
        feedbackText.text = "+" + points.ToString();

        StartCoroutine(ShowFeedbackRoutine());
    }

    public void ShowNextImageMessage()
    {
        nextImageText.gameObject.SetActive(true);
        StartCoroutine(HideNextMessageRoutine());
    }

    private IEnumerator ShowFeedbackRoutine()
    {
        // Fade In
        for (float t = 0; t < 1; t += Time.deltaTime * 2)
        {
            feedbackGroup.alpha = t;
            yield return null;
        }

        yield return new WaitForSeconds(1.2f);

        // Fade Out
        for (float t = 1; t > 0; t -= Time.deltaTime * 2)
        {
            feedbackGroup.alpha = t;
            yield return null;
        }

        feedbackGroup.alpha = 0;
    }

    private IEnumerator HideNextMessageRoutine()
    {
        yield return new WaitForSeconds(2f);

        // Animación rápida de salida (fade + desplazamiento)
        Vector3 initialPos = nextImageText.transform.localPosition;
        Vector3 targetPos = initialPos + new Vector3(0, 40, 0);

        for (float t = 0; t < 1; t += Time.deltaTime * 2)
        {
            nextImageText.alpha = 1 - t;
            nextImageText.transform.localPosition = Vector3.Lerp(initialPos, targetPos, t);
            yield return null;
        }

        nextImageText.gameObject.SetActive(false);
        nextImageText.alpha = 1;
        nextImageText.transform.localPosition = initialPos;
    }
}
