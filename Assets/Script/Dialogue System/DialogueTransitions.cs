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


}
