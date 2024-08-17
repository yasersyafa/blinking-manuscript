using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public string nameCharacter;
    public Sprite character;
    [TextArea(3, 10)] public string sentence;
}

[System.Serializable]
public class Dialogues
{
    public List<DialogueLine> dialogueLines = new List<DialogueLine>();
}

public class Dialogue : MonoBehaviour
{
    public Dialogues dialogues;
    public void TriggerDialogue()
    {
        // DialogueManager manager = FindObjectOfType<DialogueManager>();
        DialogueManager.ins.StartDialogue(dialogues);
    }

    // Method to add a dialogue line
    public void AddDialogueLine(string characterName, string sentence)
    {
        DialogueLine newLine = new DialogueLine
        {
            nameCharacter = characterName,
            sentence = sentence
        };
        dialogues.dialogueLines.Add(newLine);
    }
}
