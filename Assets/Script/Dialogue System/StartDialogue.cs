using UnityEngine;

public class StartDialogue : MonoBehaviour
{
    public DialogueLine[] dialogueLines;

    void Start()
    {
        if (GameData.directMetalurgia)
        {
            // Evitar que se muestr el diálogo
            return;
        }
        
        DialogueManager.instance.StartDialogue(dialogueLines);
    }

}
