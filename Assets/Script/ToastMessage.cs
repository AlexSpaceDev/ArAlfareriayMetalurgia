using UnityEngine;
using TMPro;
using System.Collections;

public class ToastMessage : MonoBehaviour
{
    public static ToastMessage Instance;

    public TMP_Text messageText;
    public CanvasGroup canvasGroup;

    [Header("Settings")]
    public float showDuration = 1.2f;
    public float fadeDuration = 0.4f;

    void Awake()
    {
        Instance = this;
        canvasGroup.alpha = 0;
    }

    public void Show(string msg)
    {
        messageText.text = msg;
        StopAllCoroutines();
        StartCoroutine(ShowRoutine());
    }

    IEnumerator ShowRoutine()
    {
        // Fade in
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            canvasGroup.alpha = Mathf.Lerp(0, 1, t / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = 1;

        // Stay visible
        yield return new WaitForSeconds(showDuration);

        // Fade out
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            canvasGroup.alpha = Mathf.Lerp(1, 0, t / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = 0;
    }
}
