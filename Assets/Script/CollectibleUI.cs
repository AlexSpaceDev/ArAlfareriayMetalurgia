using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CollectibleUI : MonoBehaviour
{
    [Header("References")]
    public Image buttonImage;        // Imagen oscurecida del botón (padre)
    public Image lockImage;          // Candado (hijo)

    [Header("Settings")]
    public float shakeAmount = 10f;
    public float shakeDuration = 0.2f;
    public float unlockFadeDuration = 0.3f;

    [Header("ID del coleccionable")]
    public string collectibleID = "canuto"; // puedes usar un enum luego

    private bool unlocked;
    private bool revealed;

    void Start()
    {
        LoadState();
        ApplyInitialVisualState();
    }

    void LoadState()
    {
        if (collectibleID == "canuto")
        {
            unlocked = GameData.collectibleCanutoUnlocked;
            revealed = GameData.collectibleCanutoRevealed;
        }
    }

    void SaveRevealed()
    {
        if (collectibleID == "canuto")
        {
            GameData.collectibleCanutoRevealed = true;
            SaveManager.SaveGame();
        }
    }

    // ============================
    // ESTADO INICIAL
    // ============================
    void ApplyInitialVisualState()
    {
        if (!unlocked)
        {
            // Estado bloqueado
            lockImage.gameObject.SetActive(true);
            lockImage.color = new Color(1, 1, 1, 1);
            buttonImage.color = new Color(0.3f, 0.3f, 0.3f, 1); // oscurecido
        }
        else
        {
            if (revealed)
            {
                // Ya fue revelado antes - sin animación
                lockImage.gameObject.SetActive(false);
                buttonImage.color = Color.white;
            }
            else
            {
                // Recién desbloqueado - ANIMAR
                StartCoroutine(PlayUnlockAnimation());
            }
        }
    }

    // ============================
    // ANIMACIÓN DE DESBLOQUEO
    // ============================
    IEnumerator PlayUnlockAnimation()
    {
        Vector2 originalPos = lockImage.rectTransform.anchoredPosition;

        // 1. Shake
        float elapsed = 0;
        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-shakeAmount, shakeAmount);
            float y = Random.Range(-shakeAmount * 0.5f, shakeAmount * 0.5f);

            lockImage.rectTransform.anchoredPosition =
                originalPos + new Vector2(x, y);

            elapsed += Time.deltaTime;
            yield return null;
        }
        lockImage.rectTransform.anchoredPosition = originalPos;

        // 2. Fade out candado
        Color c = lockImage.color;
        for (float t = 0; t < unlockFadeDuration; t += Time.deltaTime)
        {
            float a = Mathf.Lerp(1, 0, t / unlockFadeDuration);
            lockImage.color = new Color(c.r, c.g, c.b, a);
            yield return null;
        }
        lockImage.gameObject.SetActive(false);

        // 3. Iluminar botón
        Color start = buttonImage.color;
        for (float t = 0; t < unlockFadeDuration; t += Time.deltaTime)
        {
            buttonImage.color = Color.Lerp(start, Color.white, t / unlockFadeDuration);
            yield return null;
        }
        buttonImage.color = Color.white;

        // Guardar estado revelado
        SaveRevealed();
    }
}
