using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mindblink1 : IState
{
    private Dialogue dialogue;
    private float countdown;
    public bool isDone, isGetPassword;
    public string place, dessert, number;
    string password;
    

    public void OnEnter(GameStateManager manager)
    {
        SceneController.Ins.LoadScene("Mindblink1");
        countdown = 2f;
        isDone = false;
        
    }

    public void OnExecute(GameStateManager manager)
    {
        countdown -= Time.deltaTime;
        if(countdown <= 0 && !isDone)
        {
            isDone = true;
            dialogue = GameObject.Find("Mindblink").GetComponent<Dialogue>();
            dialogue.TriggerDialogue();
        }

        if(!string.IsNullOrEmpty(place) && !string.IsNullOrEmpty(dessert) && !string.IsNullOrEmpty(number) && !isGetPassword)
        {
            isGetPassword = true;
            GeneratePassword(place, dessert, number);
        }

        if(password != null && isGetPassword)
        {
            // next state
            manager.SetState(manager.getPassword);
        }
    }

    public void OnExit(GameStateManager manager)
    {
        manager.passwordPC = password;
    }

    public string GeneratePassword(string place, string dessert, string number)
    {
        string newPlace = GetFirstWord(place);
        string newDessert = GetFirstWord(dessert);
        string newPassword = $"{newPlace}{newDessert}{number}";
        // rumus generate password
        return password = newPassword.ToLower();
    }

    public string GetFirstWord(string input)
    {
        string[] words = input.Split(' ');
        return words[0];
    }
}
