using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideManager : MonoBehaviour
{
    public static OutsideManager ins;
    [Header("")]
    public GameObject houseOutdoor, cafeOutdoor1, officeOutdoor, cafeIndoor, officeIndoor, glitch;
    // Start is called before the first frame update
    void Start()
    {
        ins = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CafeClicked()
    {
        cafeIndoor.transform.SetAsLastSibling();
        cafeIndoor.SetActive(true);
    }

    public void CashierClicked()
    {
        if(!GameStateManager.Ins.outside1.hasGlitch)
        {
            Dialogue dialogue = GameObject.Find("cafe-before-glitch").GetComponent<Dialogue>();
            dialogue.TriggerDialogue();
            GameStateManager.Ins.outside1.mustShake = true;
        }
    }

    public void GoToCafeView()
    {
        cafeOutdoor1.transform.SetAsLastSibling();
        cafeOutdoor1.SetActive(true);
        if(!GameStateManager.Ins.outside1.isCoffeOutdoor)
        {
            Dialogue dialogue = GameObject.Find("cafe-outside").GetComponent<Dialogue>();
            dialogue.TriggerDialogue();
            GameStateManager.Ins.outside1.isCoffeOutdoor = true;
        }
        else if(GameStateManager.Ins.outside1.hasGlitch && !GameStateManager.Ins.outside1.isCoffeOutdoor2)
        {
            GameStateManager.Ins.outside1.isCoffeOutdoor2 = true;
            Dialogue dialogueAfter = GameObject.Find("cafe-outside-after").GetComponent<Dialogue>();
            dialogueAfter.TriggerDialogue();
        }
    }

    public void GoToHomeView()
    {
        houseOutdoor.transform.SetAsLastSibling();
        houseOutdoor.SetActive(true);
    }

    public void GoToOfficeView()
    {
        officeOutdoor.transform.SetAsLastSibling();
        officeOutdoor.SetActive(true);
        if(GameStateManager.Ins.outside1.hasTalkToPublisher)
        {
            Dialogue dialogue = GameObject.Find("office-after").GetComponent<Dialogue>();
            dialogue.TriggerDialogue();
            GameStateManager.Ins.outside1.cutScene = true;
        }
    }


    public void OfficeClicked()
    {
        if(GameStateManager.Ins.outside1.hasGlitch)
        {
            officeIndoor.transform.SetAsLastSibling();
            officeIndoor.SetActive(true);
            if(!GameStateManager.Ins.outside1.hasTalkToPublisher)
            {
                Dialogue dialogue = GameObject.Find("office").GetComponent<Dialogue>();
                dialogue.TriggerDialogue();
                GameStateManager.Ins.outside1.hasTalkToPublisher = true;
            }
        }
        else
        {
            ErrorDialogue("You", "I have to buy <color=#2567FF>coffee</color> first");
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
}
