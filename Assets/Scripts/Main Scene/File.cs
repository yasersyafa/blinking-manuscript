using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class File : MonoBehaviour
{
    public bool isCorrect;
    Button emailButton;
    // Start is called before the first frame update
    void Start()
    {
        emailButton = GetComponentInChildren<Button>();
        emailButton.onClick.AddListener(() => OpenAlert());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenAlert()
    {
        if(GameStateManager.Ins.currentState == GameStateManager.Ins.sendDraft)
        {
            GameObject alert = MainSceneManager.ins.alert;
            alert.SetActive(true);
            AlertHandler alertHandler = alert.GetComponent<AlertHandler>();
            alertHandler.currentFile = this;
        }
        else
        {
            MainSceneManager.ins.ErrorDialogue("You", "Not right now <color=#2567FF>{PLAYER_NAME}</color>.");
        }
    }
}
