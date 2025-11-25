using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogueTransitions : MonoBehaviour
{
    public static DialogueTransitions instance;

    [Header("Dialogue Elements")]
    public RectTransform speakerRightImage;
    public RectTransform speakerLeftImage;
    public RectTransform dialogueBox;
    public RectTransform nameBox;

    [Header("Options Elements")]
    public GameObject opcionesJuego;
    public RectTransform alfareriaBtn;
    public RectTransform metalurgiaBtn;
    public RectTransform divisionImg;

    [Header("Transition Settings")]
    public float slideTime = 0.6f;
    public float fadeTime = 0.5f;
    public float slideInTime = 0.4f;
    public float fadeInTime = 0.4f;

    [Header("Canvas Groups")]
    public CanvasGroup dialogueCanvasGroup;
    public CanvasGroup opcionesCanvasGroup;

    [Header("Option Screens")]
    public GameObject alfareriaOption;
    public CanvasGroup alfareriaCanvasGroup;

    public GameObject metalurgiaOption;
    public CanvasGroup metalurgiaCanvasGroup;

    // Save final positions
    private Vector2 alfarFinal;
    private Vector2 metalFinal;
    private Vector2 divFinal;

    private void Awake()
    {
        if (instance == null) instance = this;

        // Save final positions of options
        alfarFinal = alfareriaBtn.anchoredPosition;
        metalFinal = metalurgiaBtn.anchoredPosition;
        divFinal = divisionImg.anchoredPosition;
    }

    private void Start()
    {
        if (GameData.directMetalurgia)
        {
            StartCoroutine(ActivateDirectMetalurgia());
        }
        else if (GameData.directAlfareria)
        {
            StartCoroutine(ActivateDirectAlfareria());
        }
    }

    public IEnumerator PlayExitTransition()
    {
        // Fade and Slide out the dialogue UI
        yield return StartCoroutine(FadeAndSlideOutDialogue());

        // Disable dialogue system AFTER animation
        DialogueManager.instance.dialogueGameObject.SetActive(false);

        // Small delay
        yield return new WaitForSeconds(0.25f);

        // Slide in the options + Fade in
        yield return StartCoroutine(FadeAndSlideInOptions());

    }

    // Dialogue Transitions

    private IEnumerator FadeAndSlideOutDialogue()
    {
        // Ejecutar ambas animaciones a la vez
        IEnumerator fade = FadeOutDialogue();
        IEnumerator slide = SlideOutDialogue();

        // Iniciar ambas
        StartCoroutine(fade);
        StartCoroutine(slide);

        // Esperar hasta que ambas hayan terminado
        bool fadeDone = false;
        bool slideDone = false;

        // Cada una avisará cuando termine
        StartCoroutine(WaitFor(fade, () => fadeDone = true));
        StartCoroutine(WaitFor(slide, () => slideDone = true));

        // Esperar hasta que ambas estén listas
        while (!fadeDone || !slideDone)
            yield return null;
    }

    private IEnumerator SlideOutDialogue()
    {
        float t = 0;

        Vector2 rightStart = speakerRightImage.anchoredPosition;
        Vector2 leftStart = speakerLeftImage.anchoredPosition;
        Vector2 boxStart = dialogueBox.anchoredPosition;
        Vector2 nameStart = nameBox.anchoredPosition;

        while (t < 1)
        {
            t += Time.deltaTime / slideTime;
            float eased = Mathf.SmoothStep(0, 1, t);

            speakerRightImage.anchoredPosition = rightStart + new Vector2(1000 * eased, 0);
            speakerLeftImage.anchoredPosition = leftStart + new Vector2(-1000 * eased, 0);

            dialogueBox.anchoredPosition = boxStart + new Vector2(0, -600 * eased);
            nameBox.anchoredPosition = nameStart + new Vector2(0, -600 * eased);

            yield return null;

        }
    }

        private IEnumerator FadeOutDialogue()
    {
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime / fadeTime;
            float a = Mathf.Lerp(1f, 0f, t);

            dialogueCanvasGroup.alpha = a;

            yield return null;
        }
    }

    private IEnumerator WaitFor(IEnumerator routine, System.Action onDone)
    {
        while (routine.MoveNext())
            yield return routine.Current;
        onDone?.Invoke();
    }


    // Options Transitions
    private IEnumerator FadeAndSlideInOptions()
    {
        // Ejecutar ambas animaciones al mismo tiempo
        IEnumerator fade = FadeInOptions();
        IEnumerator slide = SlideInOptions(); // nueva función separada

        StartCoroutine(fade);
        StartCoroutine(slide);

        bool fadeDone = false;
        bool slideDone = false;

        StartCoroutine(WaitFor(fade, () => fadeDone = true));
        StartCoroutine(WaitFor(slide, () => slideDone = true));

        while (!fadeDone || !slideDone)
            yield return null;
    }

    private IEnumerator SlideInOptions()
    {
        opcionesJuego.SetActive(true);

        // Set starting positions outside screen
        alfareriaBtn.anchoredPosition = alfarFinal + new Vector2(-900, 0);
        metalurgiaBtn.anchoredPosition = metalFinal + new Vector2(900, 0);
        divisionImg.anchoredPosition = divFinal + new Vector2(0, 800);

        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime / slideInTime;
            float eased = Mathf.SmoothStep(0, 1, t);

            alfareriaBtn.anchoredPosition = Vector2.Lerp(alfareriaBtn.anchoredPosition, alfarFinal, eased);
            metalurgiaBtn.anchoredPosition = Vector2.Lerp(metalurgiaBtn.anchoredPosition, metalFinal, eased);
            divisionImg.anchoredPosition = Vector2.Lerp(divisionImg.anchoredPosition, divFinal, eased);

            yield return null;
        }
    }

    private IEnumerator FadeInOptions()
    {
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime / fadeInTime;
            opcionesCanvasGroup.alpha = Mathf.Lerp(0, 1, t);
            yield return null;
        }
    }

    // Animación inversa para volver a mostrar opciones
    private IEnumerator FadeAndSlideOutOptions()
    {
        IEnumerator fade = FadeOutOpciones();
        IEnumerator slide = SlideOutOpciones();

        StartCoroutine(fade);
        StartCoroutine(slide);

        bool fadeDone = false;
        bool slideDone = false;

        StartCoroutine(WaitFor(fade, () => fadeDone = true));
        StartCoroutine(WaitFor(slide, () => slideDone = true));

        while (!fadeDone || !slideDone)
            yield return null;

        opcionesJuego.SetActive(false);
    }

    private IEnumerator FadeOutOpciones()
    {
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime / fadeInTime;
            opcionesCanvasGroup.alpha = Mathf.Lerp(1, 0, t);
            yield return null;
        }
    }

    private IEnumerator SlideOutOpciones()
    {
        float t = 0;

        Vector2 alfarStart = alfareriaBtn.anchoredPosition;
        Vector2 metalStart = metalurgiaBtn.anchoredPosition;
        Vector2 divStart = divisionImg.anchoredPosition;

        while (t < 1)
        {
            t += Time.deltaTime / slideInTime;
            float eased = Mathf.SmoothStep(0, 1, t);

            alfareriaBtn.anchoredPosition = alfarStart + new Vector2(-900 * eased, 0);
            metalurgiaBtn.anchoredPosition = metalStart + new Vector2(900 * eased, 0);
            divisionImg.anchoredPosition = divStart + new Vector2(0, 800 * eased);

            yield return null;
        }
    }


    // Animación para mostrar la pantalla seleccionada

    // Abrir la pantalla de Alfarería
    public void OnAlfareriaPressed()
    {
        StartCoroutine(OpenOptionScreen(alfareriaOption, alfareriaCanvasGroup));
    }

    // Abrir la pantalla de Metalurgia
    public void OnMetalurgiaPressed()
    {
        StartCoroutine(OpenOptionScreen(metalurgiaOption, metalurgiaCanvasGroup));
    }

    // Transición para abrir la pantalla de opción seleccionada
    private IEnumerator OpenOptionScreen(GameObject optionGO, CanvasGroup optionCG)
    {
        // Primero ocultamos las opciones generales
        yield return StartCoroutine(FadeAndSlideOutOptions());

        // Activamos el panel seleccionado
        optionGO.SetActive(true);
        optionCG.alpha = 0;

        // Fade In suave
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / 0.4f;
            optionCG.alpha = Mathf.Lerp(0, 1, t);
            yield return null;
        }
    }


    // Volver a las opciones principales desde una pantalla de opción
    public void OnBackToOptions()
    {
        StartCoroutine(ReturnToMainOptions());
    }

    private IEnumerator ReturnToMainOptions()
    {
        // 1. Fade Out del panel activo
        CanvasGroup activeGroup = null;
        GameObject activeGO = null;

        if (alfareriaOption.activeSelf)
        {
            activeGO = alfareriaOption;
            activeGroup = alfareriaCanvasGroup;
        }
        else if (metalurgiaOption.activeSelf)
        {
            activeGO = metalurgiaOption;
            activeGroup = metalurgiaCanvasGroup;
        }

        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / 0.4f;
            activeGroup.alpha = Mathf.Lerp(1, 0, t);
            yield return null;
        }

        activeGO.SetActive(false);

        // 2. Mostrar de nuevo opciones
        yield return StartCoroutine(FadeAndSlideInOptions());
    }

    // Corrutina para activar directamente Metalurgia si se indicó
    private IEnumerator ActivateDirectMetalurgia()
    {
        // Desactivar diálogo completamente
        DialogueManager.instance.dialogueGameObject.SetActive(false);

        // Desactivar opciones del medio
        opcionesJuego.SetActive(false);

        yield return null; // un frame para que todo inicialice

        // Activar panel Metalurgia directamente
        metalurgiaOption.SetActive(true);
        metalurgiaCanvasGroup.alpha = 1;

        // MUY IMPORTANTE:
        // Resetear el flag para que no afecte la próxima vez que entres
        GameData.directMetalurgia = false;
    }

    // Corrutina para activar directamente Alfarería si se indicó
    private IEnumerator ActivateDirectAlfareria()
    {
        // Desactivar diálogo completamente
        DialogueManager.instance.dialogueGameObject.SetActive(false);

        // Desactivar opciones del medio
        opcionesJuego.SetActive(false);

        yield return null; // un frame para que todo inicialice

        // Activar panel Alfareria directamente
        alfareriaOption.SetActive(true);
        alfareriaCanvasGroup.alpha = 1;

        // MUY IMPORTANTE;
        // Resetear el flag para que no afecte la próxima vez que entres
        GameData.directAlfareria = false;
    }

}
