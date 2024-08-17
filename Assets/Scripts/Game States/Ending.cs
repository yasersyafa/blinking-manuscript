using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : IState
{
    private float countdownDialogue1;
    private bool isSiwtchFrame, isSiwtchFrameframe2, isSiwtchFrame3;
    private float timerSwitch;
    public bool isDialogue1 = false;
    public bool isDialogue2 = false;
    public Dialogue dialogue1, dialogue2;
    public bool isEnd = false;
    public void OnEnter(GameStateManager manager)
    {
        SceneController.Ins.LoadScene("Cutscene Ending");
        
        countdownDialogue1 = 1.5f;
    }

    public void OnExecute(GameStateManager manager)
    {
        if(SceneManager.GetActiveScene().name == "Cutscene Ending")
        {
            if(!isDialogue1)
            {
                countdownDialogue1 -= Time.deltaTime;
                if(countdownDialogue1 <= 0)
                {
                    isDialogue1 = true;
                    dialogue1 = GameObject.Find("Dialogue1").GetComponent<Dialogue>();
                    dialogue1.TriggerDialogue();
                }
            }
            if(isDialogue1 && DialogueManager.ins.isDone)
            {
                if(!isSiwtchFrame)
                {
                    isSiwtchFrame = true;
                    SetImage.instance.StartAnimation();
                }
            }
            if(SetImage.instance.isFirst && !isDialogue2)
            {
                isDialogue2 = true;
                dialogue2 = GameObject.Find("Dialogue2").GetComponent<Dialogue>();
                dialogue2.TriggerDialogue();
            }

            if(isDialogue2 && DialogueManager.ins.isDone && !isEnd)
            {
                isEnd = true;
                // set animasi 2
                SetImage.instance.Animator2();
            }
        }
        
    }

    public void OnExit(GameStateManager manager)
    {
        
    }
}
