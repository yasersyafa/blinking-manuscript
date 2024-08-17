using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Mindblink1Manager : MonoBehaviour
{
    public TMP_InputField numberField, placeField, dessertField;
    public GameObject bgFieldNumber, bgFieldPlace, bgFieldDessert;
    public Item place, number, dessert;
    
    
    // public Button numberButton, placeButton, dessertButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackButtonClicked()
    {
        IState state = GameStateManager.Ins.currentState;
        if(state == GameStateManager.Ins.getPassword)
        {
            SceneController.Ins.LoadScene("Main Scene");
            // GameStateManager.Ins.SetState()
        }
        else
        {
            ErrorDialogue("You", "I should <color=#2567FF>find something</color> to recall my memory about the password…");
        }
    }

    public void SaveNumber()
    {
        if(!string.IsNullOrEmpty(numberField.text))
        {
            GameStateManager.Ins.mindblink1.number = numberField.text;
            bgFieldNumber.SetActive(false);
            number.isCollect = true;
        }
    }

    public void SavePlace()
    {
        if(!string.IsNullOrEmpty(placeField.text))
        {
            GameStateManager.Ins.mindblink1.place = placeField.text;
            bgFieldPlace.SetActive(false);
            place.isCollect = true;
        }
    }

    public void SaveDessert()
    {
        if(!string.IsNullOrEmpty(dessertField.text))
        {
            GameStateManager.Ins.mindblink1.dessert = dessertField.text;
            bgFieldDessert.SetActive(false);
            dessert.isCollect = true;
        }
    }
    public void ErrorDialogue(string name, string sentence)
    {
        if(DialogueManager.ins.isDone)
        {
            Dialogue dialogue = GameObject.Find("ErrorDialogue").GetComponent<Dialogue>();
            dialogue.dialogues.dialogueLines.Clear();
            dialogue.AddDialogueLine(name, sentence);
            dialogue.TriggerDialogue();
        }
    }
}
