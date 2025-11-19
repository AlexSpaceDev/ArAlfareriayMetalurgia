using UnityEngine;

public class StartDialogue : MonoBehaviour
{
    public DialogueLine[] dialogueLines;

    void Start()
    {
        DialogueManager.instance.StartDialogue(dialogueLines);
    }

}
