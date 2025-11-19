using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    //SINGLETON: puede ser llamado sin necesidad de una referencia directa
    public static DialogueManager instance;

    [Header("Linked Components")]
    public TextMeshProUGUI nameBox;
    public TextMeshProUGUI textBox;
    public GameObject dialogueGameObject;
    public Image speakerImage;

    [Header("Speakers")]
    public Image speakerLeftImage;
    public Image speakerRightImage;

    public float activeScale = 0.22f;
    public float inactiveScale = 0.2f;

    public Color activeColor = Color.white;
    public Color inactiveColor = new Color(0.4f, 0.4f, 0.4f);

    [Header("Text Configuration")]
    public float typingSpeed = 0.05f;

    [Header("Dialogue Status")]
    public bool isTyping = false;
    public bool dialogueFinished = true;

    [Header("Dialogue")]
    public DialogueLine[] dialogueLines;

    [Header("Next Dialogue Icon")]
    public RectTransform nextDialogueIcon;
    public float arrowMoveDistance = 10f;
    public float arrowMoveSpeed = 2f;

    #region PRIVATE VARIABLES
    private int currentIndex = 0;

    private Coroutine typingCoroutine;
    private bool justStarted = false;

    private float clickCooldown = 0.1f;
    private float lastClickTime = -1f;
    #endregion

    private InputSystem_Actions actions;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }

        actions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        // Activar mapa UI
        actions.UI.Enable();

        // Escuchar acción de Click (mouse o touch)
        actions.UI.Click.performed += OnClickPerformed;
    }

    private void OnDisable()
    {
        actions.UI.Click.performed -= OnClickPerformed;
        actions.UI.Disable();
    }

    private void OnClickPerformed(InputAction.CallbackContext context)
    {
        // Evita doble click instantáneo (bug de diálogos cortos)
        if (Time.time - lastClickTime < clickCooldown)
            return;

        lastClickTime = Time.time;
        

        if (justStarted)
        {
            justStarted = false;
            return;
        }

        if (isTyping)
        {
            StopCoroutine(typingCoroutine);
            ShowFullLine(dialogueLines[currentIndex]);
            isTyping = false;
        } else
        {
            nextDialogueIcon.gameObject.SetActive(false);
            currentIndex++;

            if (currentIndex < dialogueLines.Length)
            {
                typingCoroutine = StartCoroutine(TypeLine(dialogueLines[currentIndex]));
            } else
            {
                textBox.text = "";
                nameBox.text = "";
                speakerImage.sprite = null;
                dialogueFinished = true;
                dialogueGameObject.SetActive(false);
            }
        }
    }

    private void UpdateSpeakerVisuals(int speakingID, Sprite sprite)
    {
        // Asignar imágenes si se envió sprite
        if (sprite != null)
        {
            if (speakingID == 0) speakerLeftImage.sprite = sprite;
            else speakerRightImage.sprite = sprite;
        }

        // Cambiar escala y color
        if (speakingID == 0)
        {
            // Left habla
            speakerLeftImage.transform.localScale = Vector3.one * activeScale;
            speakerLeftImage.color = activeColor;

            // Right se oscurece
            speakerRightImage.transform.localScale = Vector3.one * inactiveScale;
            speakerRightImage.color = inactiveColor;
        }
        else if (speakingID == 1)
        {
            // Right habla
            speakerRightImage.transform.localScale = Vector3.one * activeScale;
            speakerRightImage.color = activeColor;

            // Left se oscurece
            speakerLeftImage.transform.localScale = Vector3.one * inactiveScale;
            speakerLeftImage.color = inactiveColor;
        }
    }

    private void Update()
    {
        if (nextDialogueIcon != null && nextDialogueIcon.gameObject.activeSelf)
        {
            float offset = Mathf.Sin(Time.time * arrowMoveSpeed) * arrowMoveDistance;
            nextDialogueIcon.anchoredPosition = new Vector2(
                nextDialogueIcon.anchoredPosition.x,
                offset
            );
        }
    }

    public void StartDialogue(DialogueLine[] newLines)
    {
        dialogueGameObject.SetActive(true);
        dialogueFinished = false;
        dialogueLines = newLines;
        currentIndex = 0;
        justStarted = true;
        nextDialogueIcon.gameObject.SetActive(false);
        typingCoroutine = StartCoroutine(TypeLine(dialogueLines[currentIndex]));
    }

    IEnumerator TypeLine(DialogueLine line)
    {
        isTyping = true;

        textBox.text = "";
        nameBox.text = line.speakerName;
        // NUEVO: actualizar 2 speakers en pantalla
        UpdateSpeakerVisuals(line.speakerID, line.speakerPortrait);

        foreach (char c in line.dialogueText)
        {
            textBox.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
        nextDialogueIcon.gameObject.SetActive(true);
    }

    private void ShowFullLine(DialogueLine line)
    {
        textBox.text = line.dialogueText;
        nameBox.text = line.speakerName;
        UpdateSpeakerVisuals(line.speakerID, line.speakerPortrait);
        // NUEVO: mostrar flecha cuando se revela todo el texto
        nextDialogueIcon.gameObject.SetActive(true);
    }

}
