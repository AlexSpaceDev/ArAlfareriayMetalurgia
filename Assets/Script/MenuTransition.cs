using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuTransition : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainMenuPanel;
    public GameObject introPanel;
    public GameObject endingPanel;

    [Header("Main Menu Elements")]
    public RectTransform imageMain;
    public RectTransform buttonsGroup;

    [Header("Intro Elements")]
    public CanvasGroup introCanvas;

    [Header("Ending Elements")]
    public CanvasGroup endingCanvas;

    // --- SETTINGS ---
    public float moveDuration = 0.8f;
    public float fadeDuration = 0.6f;

    // --- INTERNAL STATE ---
    private Vector2 originalImagePos;
    private Vector2 originalButtonsPos;
    private GameObject currentActivePanel;
    private CanvasGroup currentActiveCanvas;

    void Start()
    {
        // Guardamos las posiciones iniciales del menú
        originalImagePos = imageMain.anchoredPosition;
        originalButtonsPos = buttonsGroup.anchoredPosition;

        // El menú principal inicia como panel activo
        currentActivePanel = null;
        currentActiveCanvas = null;
    }

    // ---------------------------
    //  TRANSICIÓN PLAY
    // ---------------------------
    public void OnPlayPressed()
    {
        currentActivePanel = introPanel;
        currentActiveCanvas = introCanvas;

        StartCoroutine(GeneralTransition(introPanel, introCanvas));
    }

    // ---------------------------
    //  TRANSICIÓN FINALES
    // ---------------------------
    public void OnEndingsPressed()
    {
        currentActivePanel = endingPanel;
        currentActiveCanvas = endingCanvas;

        StartCoroutine(GeneralTransition(endingPanel, endingCanvas));
    }

    // ---------------------------
    //  LÓGICA GENERAL DE TRANSICIÓN
    // ---------------------------
    IEnumerator GeneralTransition(GameObject targetPanel, CanvasGroup targetCanvas)
    {
        // 1. Animación de salida del Main Menu
        StartCoroutine(MoveUI(imageMain, new Vector2(originalImagePos.x, -800), moveDuration));
        StartCoroutine(MoveUI(buttonsGroup, new Vector2(-800, originalButtonsPos.y), moveDuration));

        // Fade out del panel principal
        CanvasGroup mainGroup = mainMenuPanel.GetComponent<CanvasGroup>();
        if (mainGroup == null)
            mainGroup = mainMenuPanel.AddComponent<CanvasGroup>();

        yield return StartCoroutine(FadeCanvas(mainGroup, 1, 0, fadeDuration));

        mainMenuPanel.SetActive(false);

        // 2. Activar panel destino
        targetPanel.SetActive(true);
        targetCanvas.alpha = 0;

        yield return StartCoroutine(FadeCanvas(targetCanvas, 0, 1, fadeDuration));
    }

    // ---------------------------
    //     TRANSICIÓN INVERSA
    // ---------------------------
    public void OnBackPressed()
    {
        if (currentActivePanel != null)
            StartCoroutine(ReturnToMainMenu());
    }

    IEnumerator ReturnToMainMenu()
    {
        // 1. Fade out del panel actual
        yield return StartCoroutine(FadeCanvas(currentActiveCanvas, 1, 0, fadeDuration));

        currentActivePanel.SetActive(false);

        // 2. Volver a mostrar el menú principal
        mainMenuPanel.SetActive(true);

        CanvasGroup mainGroup = mainMenuPanel.GetComponent<CanvasGroup>();
        if (mainGroup == null)
            mainGroup = mainMenuPanel.AddComponent<CanvasGroup>();

        mainGroup.alpha = 0;

        // Recolocar elementos del menú fuera de pantalla antes de animarlos
        imageMain.anchoredPosition = new Vector2(originalImagePos.x, -800);
        buttonsGroup.anchoredPosition = new Vector2(-800, originalButtonsPos.y);

        // Animación de entrada
        StartCoroutine(MoveUI(imageMain, originalImagePos, moveDuration));
        StartCoroutine(MoveUI(buttonsGroup, originalButtonsPos, moveDuration));

        yield return StartCoroutine(FadeCanvas(mainGroup, 0, 1, fadeDuration));

        // Limpieza
        currentActivePanel = null;
        currentActiveCanvas = null;
    }

    // --- UTILIDADES ---
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
        SceneManager.LoadScene("Prologue");
    }
}

