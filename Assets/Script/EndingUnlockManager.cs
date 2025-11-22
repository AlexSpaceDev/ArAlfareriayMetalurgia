using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class EndingUnlockManager : MonoBehaviour
{
    [System.Serializable]
    public class EndingUI
    {
        public string id;                 // "A", "B", "C", "D"
        public Button button;             // el botón del final
        public Image buttonImage;         // imagen principal del botón
        public Image lockImage;           // imagen del candado
        public CanvasGroup group;         // CanvasGroup para fade
        public bool unlocked;             // juego lo llena
        public bool revealed;             // si ya se reveló antes
    }

    [Header("General Unlock Settings")]
    public float unlockFadeDuration = 0.8f;
    public float shakeAmount = 12f;
    public float shakeDuration = 0.25f;

    [Header("Secret Ending Movement")]
    public RectTransform[] elementsToMove;    // Text, A, B, C
    public float secretEndingMoveY = 120f;    // cuánto suben
    public float secretMoveSpeed = 0.6f;

    [Header("Ending Items")]
    public EndingUI[] endings;

    private EndingUI endingD;
    private bool initialized = false;

    void OnEnable()
    {
        if (!initialized)
        {
            InitializeStates();
            initialized = true;
        }

        CheckAndRevealEndings();
    }

    /* ===========================================================
                      INITIAL STATES
    =========================================================== */
    void InitializeStates()
    {
        foreach (var e in endings)
        {
            e.unlocked = GetUnlockedState(e.id);
            e.revealed = GetRevealedState(e.id);

            // Obtener canvas group
            if (e.group == null)
                e.group = e.button.GetComponent<CanvasGroup>();

            if (e.id == "D")
                endingD = e;

            if (e.id == "D" && !e.unlocked)
            {
                // Final D secreto → comienza invisible
                e.group.alpha = 0;
                e.button.interactable = false;
                e.lockImage.gameObject.SetActive(true);
                continue;
            }

            if (e.unlocked && e.revealed)
            {
                e.buttonImage.color = Color.white;
                e.lockImage.gameObject.SetActive(false);
            }
            else
            {
                e.buttonImage.color = new Color(0.5f, 0.5f, 0.5f);
                e.lockImage.gameObject.SetActive(true);
            }
        }
    }

    /* ===========================================================
                 CHECK NORMAL UNLOCK + SECRET D
    =========================================================== */
    void CheckAndRevealEndings()
    {
        // A / B / C
        foreach (var e in endings)
        {
            if (e.id != "D" && e.unlocked && !e.revealed)
                StartCoroutine(PlayUnlockAnimation(e));
        }

        // FINAL D → su lógica es diferente
        if (endingD.unlocked && !endingD.revealed)
        {
            StartCoroutine(RevealSecretEndingD());
        }
    }

    /* ===========================================================
                 NORMAL UNLOCK ANIMATION (A/B/C)
    =========================================================== */
    IEnumerator PlayUnlockAnimation(EndingUI e)
    {
        // 1. Auto shake
        Vector2 originalPos = e.lockImage.rectTransform.anchoredPosition;
        float elapsed = 0;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-shakeAmount, shakeAmount);
            float y = Random.Range(-shakeAmount * 0.5f, shakeAmount * 0.5f);

            e.lockImage.rectTransform.anchoredPosition = originalPos + new Vector2(x, y);
            elapsed += Time.deltaTime;
            yield return null;
        }
        e.lockImage.rectTransform.anchoredPosition = originalPos;

        // 2. Fade out del candado
        Color lc = e.lockImage.color;
        for (float t = 0; t < unlockFadeDuration; t += Time.deltaTime)
        {
            float a = Mathf.Lerp(1, 0, t / unlockFadeDuration);
            e.lockImage.color = new Color(lc.r, lc.g, lc.b, a);
            yield return null;
        }
        e.lockImage.gameObject.SetActive(false);

        // 3. Iluminar el botón
        Color initial = e.buttonImage.color;
        for (float t = 0; t < unlockFadeDuration; t += Time.deltaTime)
        {
            e.buttonImage.color = Color.Lerp(initial, Color.white, t / unlockFadeDuration);
            yield return null;
        }
        e.buttonImage.color = Color.white;

        // 4. Marca como revelado
        SetRevealedState(e.id, true);
        e.revealed = true;
    }

    /* ===========================================================
                  SPECIAL SECRET ENDING D ANIMATION
    =========================================================== */
    IEnumerator RevealSecretEndingD()
    {
        // 1. Mover todos los elementos hacia arriba
        Vector2[] startPositions = new Vector2[elementsToMove.Length];
        for (int i = 0; i < elementsToMove.Length; i++)
            startPositions[i] = elementsToMove[i].anchoredPosition;

        float t = 0;
        while (t < secretMoveSpeed)
        {
            float lerp = t / secretMoveSpeed;

            for (int i = 0; i < elementsToMove.Length; i++)
            {
                elementsToMove[i].anchoredPosition =
                    startPositions[i] + new Vector2(0, secretEndingMoveY * lerp);
            }

            t += Time.deltaTime;
            yield return null;
        }

        // 2. Fade in del botón del Final D
        endingD.group.alpha = 0;
        endingD.button.interactable = true;

        for (float f = 0; f < unlockFadeDuration; f += Time.deltaTime)
        {
            endingD.group.alpha = Mathf.Lerp(0, 1, f / unlockFadeDuration);
            yield return null;
        }

        endingD.group.alpha = 1;

        // 3. Ejecutar animación normal de desbloqueo
        yield return StartCoroutine(PlayUnlockAnimation(endingD));

        // 4. Marcar como revelado
        SetRevealedState("D", true);
        endingD.revealed = true;
    }

    /* ===========================================================
                      GETTERS / SETTERS
    =========================================================== */
    bool GetUnlockedState(string id)
    {
        return id switch
        {
            "A" => GameData.finalAUnlocked,
            "B" => GameData.finalBUnlocked,
            "C" => GameData.finalCUnlocked,
            "D" => GameData.finalDUnlocked,
            _ => false
        };
    }

    bool GetRevealedState(string id)
    {
        return id switch
        {
            "A" => GameData.finalARevealed,
            "B" => GameData.finalBRevealed,
            "C" => GameData.finalCRevealed,
            "D" => GameData.finalDRevealed,
            _ => false
        };
    }

    void SetRevealedState(string id, bool val)
    {
        switch (id)
        {
            case "A": GameData.finalARevealed = val; break;
            case "B": GameData.finalBRevealed = val; break;
            case "C": GameData.finalCRevealed = val; break;
            case "D": GameData.finalDRevealed = val; break;
        }
    }
}

