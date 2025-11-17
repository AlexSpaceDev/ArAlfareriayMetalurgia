using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuTransition : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainMenuPanel;
    public GameObject introPanel;

    [Header("Main Menu Elements")]
    public RectTransform imageMain;
    public RectTransform buttonsGroup;

    [Header("Intro Elements")]
    public CanvasGroup introCanvas;
    public Button startButton;

    // --- SETTINGS ---
    public float moveDuration = 0.8f;
    public float fadeDuration = 0.6f;

    public void OnPlayPressed()
    {
        StartCoroutine(PlayTransition());
    }

    IEnumerator PlayTransition()
    {
        // 1. Animación de salida del Main Menu
        StartCoroutine(MoveUI(imageMain, new Vector2(imageMain.anchoredPosition.x, -800), moveDuration));
        StartCoroutine(MoveUI(buttonsGroup, new Vector2(-800, buttonsGroup.anchoredPosition.y), moveDuration));

        // Pequeño fade-out del panel
        CanvasGroup mainGroup = mainMenuPanel.AddComponent<CanvasGroup>();
        yield return StartCoroutine(FadeCanvas(mainGroup, 1, 0, fadeDuration));

        mainMenuPanel.SetActive(false);

        // 2. Activar panel de introducción
        introPanel.SetActive(true);
        introCanvas.alpha = 0;

        yield return StartCoroutine(FadeCanvas(introCanvas, 0, 1, fadeDuration));
    }

    // --- UTILIDADES DE ANIMACIÓN ---
    IEnumerator MoveUI(RectTransform rect, Vector2 target, float duration)
    {
        Vector2 start = rect.anchoredPosition;
        float time = 0;

        while (time < duration)
        {
            rect.anchoredPosition = Vector2.Lerp(start, target, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        rect.anchoredPosition = target;
    }

    IEnumerator FadeCanvas(CanvasGroup group, float from, float to, float duration)
    {
        float time = 0;

        while (time < duration)
        {
            group.alpha = Mathf.Lerp(from, to, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        group.alpha = to;
    }

    public void OnStartGame()
    {
        // Aquí cargas la escena AR
        SceneManager.LoadScene("SampleScene");
    }
}
