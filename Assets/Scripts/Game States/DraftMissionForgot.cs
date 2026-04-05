using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraftMissionForgot : IState, ICalendarHandler
{
    private Dialogue dialogue;
    public bool isDone;
    public bool hasClickedCalendar;
    private float countdown = 0.3f;
    public void OnEnter(GameStateManager manager)
    {
        dialogue = GameObject.Find("DraftForgot").GetComponent<Dialogue>();
        isDone = false;
        countdown = .3f;
    }

    public void OnExecute(GameStateManager manager)
    {
        countdown -= Time.deltaTime;
        if(countdown <= 0 && !isDone)
        {
            isDone = true;
            dialogue.TriggerDialogue();
        }
        if(hasClickedCalendar && DialogueManager.ins.isDone)
        {
            manager.SetState(manager.mindblink2);
        }
    }

    public void OnExit(GameStateManager manager)
    {
        
    }

    public void OnCalendarClicked(GameStateManager manager)
    {
        var obj = GameObject.Find("CalendarDialogue");
        if (obj == null)
        {
            Debug.LogError("[DraftMissionForgot] 'CalendarDialogue' tidak ditemukan!");
            return;
        }
        obj.GetComponent<Dialogue>()?.TriggerDialogue();
        hasClickedCalendar = true;
    }
}
