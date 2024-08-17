using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Mindblink2Manager : MonoBehaviour
{
    public static Mindblink2Manager ins;
    public Field[] fields;
    public ChoicePopUp popUp;
    // Start is called before the first frame update
    void Start()
    {
        ins = this;
    }

    public void BackButton()
    {
        IState currentState = GameStateManager.Ins.currentState;
        if(currentState == GameStateManager.Ins.draftMissionComplete)
        {
            // next state
            GameStateManager.Ins.draftMissionComplete.hasDone = true;
        }
        else
        {
            ErrorDialogue("You", "I have to solve the puzzle...");
        }
    }

    public void ErrorDialogue(string name, string sentence)
    {
        if(DialogueManager.ins.isDone)
        {
            Dialogue dialogue = GameObject.Find("ErrorDialogue").GetComponent<Dialogue>();
            if(dialogue != null)
            {
                dialogue.dialogues.dialogueLines.Clear();
                dialogue.AddDialogueLine(name, sentence);
                dialogue.TriggerDialogue();
            }
            else {
                Debug.Log("tidak ditemukan");
            }
        }
        else Debug.Log("not done");
    }

    public void CheckResult()
    {
        BlockHasFilled(result => {
            if (result == "2212")
            {
                GameStateManager.Ins.mindblink2.isComplete = true;
            }
            else
            {
                ErrorDialogue("You", "Something is just… not right, I guess.");
                // clear
                ClearAllSlot();
            }
        });
    }

    void ClearAllSlot()
    {
        foreach(Field field in fields)
        {
            field.isFilled = false;
            TextMeshProUGUI textMesh = field.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            textMesh.text = "";
        }
    }

    private void BlockHasFilled(Action<string> handler)
    {
        string result = "";
        foreach(Field field in fields)
        {
            if (!field.isFilled) return;
            TextMeshProUGUI textMesh = field.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            result += textMesh.text;
        }
        handler(result);
    }
}
