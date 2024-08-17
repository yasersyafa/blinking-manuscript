using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public AudioClip[] keyboardSfx;
    public AudioSource keyboardSource;
    public static DialogueManager ins;
    public GameObject bgDialogue;
    public TextMeshProUGUI characterName, dialogueText;
    public Image characterIcon;
    [HideInInspector] public bool isDone = true;
    private Queue<DialogueLine> lines;
    
    void Start()
    {
        ins = this;
        lines = new Queue<DialogueLine>();
    }

    public void StartDialogue(Dialogues dialogues)
    {
        isDone = false;
        bgDialogue.SetActive(true);
        lines.Clear();
        foreach(DialogueLine dialogueLine in dialogues.dialogueLines)
        {
            lines.Enqueue(dialogueLine);
        }
        DisplayDialogue();
    }

    public void DisplayDialogue()
    {
        if(lines.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueLine currentLine = lines.Dequeue();
        characterName.text = currentLine.nameCharacter;
        if(currentLine.character != null)
        {
            characterIcon.sprite = currentLine.character;
        }
        string modifiedSentence = currentLine.sentence;
        if(modifiedSentence.Contains("{PLAYER_NAME}"))
        {
            modifiedSentence = modifiedSentence.Replace("{PLAYER_NAME}", GameStateManager.Ins.playerName);
        }
        else if(modifiedSentence.Contains("{PASSWORD}"))
        {
            modifiedSentence = modifiedSentence.Replace("{PASSWORD}", GameStateManager.Ins.passwordPC);
        }
        StopAllCoroutines();
        StartCoroutine(TypeSentence(modifiedSentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        
        bool isBold = false;
        dialogueText.text = "";
        foreach(char c in sentence)
        {
            AudioClip clip = GetRandomClips();
            dialogueText.text += c;
            keyboardSource.PlayOneShot(clip);
            if(c == '<')
            {
                isBold = true;
            }
            else if(c == '>')
            {
                isBold = false;
            }

            if(!isBold)
            {
                yield return new WaitForSeconds(0.03f);
            }
        }
    }

    void EndDialogue()
    {
        StopAllCoroutines();
        Debug.Log("Done");
        bgDialogue.SetActive(false);
        isDone = true;
    }

    public AudioClip GetRandomClips()
    {
        int index = Random.Range(0, keyboardSfx.Length);
        AudioClip newClip = keyboardSfx[index];
        return newClip;
    }

}
