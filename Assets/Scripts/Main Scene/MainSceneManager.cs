using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneManager : MonoBehaviour
{
    public static MainSceneManager ins;
    [Header("Canvas")]
    public GameObject canvasTable;
    public GameObject cavasRoom;
    public GameObject canvasFocused;
    [Space]
    [Header("App")]
    public GameObject mxWriter;
    public GameObject MxWriter;
    public GameObject FileManager;
    public GameObject fileManager;

    [Space]
    public bool isDone;
    [Header("Mini PC")]
    public GameObject lockScreen;
    public GameObject shutdownScreen;

    [Header("Focused PC")]
    public GameObject screenLocked;
    public GameObject screenShutdown;

    [Header("Phone")]
    public GameObject handphone;

    [Header("Input Name")]
    public TMP_InputField inputField;
    public GameObject bgInputField;

    [Header("Password Field")]
    public TMP_InputField passwordField;
    public Button passwordButton;

    [Header("Bathroom Canvas")]
    public GameObject bgBathroom;
    public Button doorBathroom, sinkButton;
    [Header("Alert Mail")]
    public GameObject alert;
    [Space]

    // buttons
    public Button powerOff, stopHandphone, nameButton, bathroomButton, doorOutsideButton;

    private void Awake()
    {
        ins = this;
    }

    private void Start()
    {
        powerOff.onClick.AddListener(() => ShutdownComputer());
        stopHandphone.onClick.AddListener(() => StopShakingPhone());
        nameButton.onClick.AddListener(() => InputPlayerName());
        bathroomButton.onClick.AddListener(() => GoToBathroom());
        doorOutsideButton.onClick.AddListener(() => GoOutside());
        doorBathroom.onClick.AddListener(() => BackToRoom());
        sinkButton.onClick.AddListener(() => SinkClicked());
    }
    public void ShutdownComputer()
    {
        if(GameStateManager.Ins.currentState == GameStateManager.Ins.computerScene)
        {
            lockScreen.SetActive(true);
            shutdownScreen.SetActive(true);
            screenLocked.SetActive(true);
            screenShutdown.SetActive(true);
            // pindah state ke telephone 1
            GameStateManager.Ins.SetState(GameStateManager.Ins.callOne);
        }
        else if(GameStateManager.Ins.currentState == GameStateManager.Ins.publisherTwo)
        {
            
            shutdownScreen.SetActive(false);
            screenShutdown.SetActive(false);
            GameStateManager.Ins.SetState(GameStateManager.Ins.forgotPassword);
        }
        else if(GameStateManager.Ins.currentState == GameStateManager.Ins.endingBegin)
        {
            GameStateManager.Ins.SetState(GameStateManager.Ins.mindblink3);
        }
        else if(GameStateManager.Ins.currentState == GameStateManager.Ins.endingCompleted)
        {
            // next state
            GameStateManager.Ins.SetState(GameStateManager.Ins.day3);
        }
        // else if(GameStateManager.Ins.currentState == GameStateManager.Ins.day2)
        // {
        //     shutdownScreen.SetActive(false);
        //     screenShutdown.SetActive(false);
        // }
        else
        {
            ErrorDialogue("You", "There is something I want to do it right now");
            
        }
    }

    public void StopShakingPhone()
    {
        SoundEffect.Ins.audioSource.Stop();
        GameObject effect = handphone.transform.GetChild(handphone.transform.childCount - 1).gameObject;
        if(GameStateManager.Ins.callOne.isDone)
        {
            effect.SetActive(false);
            GameStateManager.Ins.callOne.isShaking = false;
        }
        if(GameStateManager.Ins.currentState == GameStateManager.Ins.callTwo)
        {
            effect.SetActive(false);
            GameStateManager.Ins.callTwo.isShaking = false;
        }
        else 
        {
            // ErrorDialogue("You", "Nobody calls you");
        }
    }

    public void InputPlayerName()
    {
        
        if(GameStateManager.Ins.currentState == GameStateManager.Ins.inputNamePlayer)
        {
            GameStateManager.Ins.playerName = inputField.text;
            GameStateManager.Ins.inputNamePlayer.isFilled = true;
        }
    }

    public void EnterPassword()
    {
        if(GameStateManager.Ins.currentState == GameStateManager.Ins.getPassword)
        {
            if(string.IsNullOrEmpty(passwordField.text))
            {
                ErrorDialogue("You", "Oh No! The password is incorrect");
            }
            else
            {
                if(passwordField.text != GameStateManager.Ins.passwordPC)
                {
                    ErrorDialogue("You", "Hmm, the password is <color=#2567FF>{PASSWORD}</color>... Maybe I typed it wrong.");
                }
                else
                {
                    // false active locked screen
                    lockScreen.SetActive(false);
                    screenLocked.SetActive(false);
                    // set active all apps
                    MxWriter.SetActive(false);
                    mxWriter.SetActive(false);
                    FileManager.SetActive(false);
                    fileManager.SetActive(false);
                    GameStateManager.Ins.SetState(GameStateManager.Ins.draftMissionBegin);
                }
            }
        }
        // else if(GameStateManager.Ins.currentState == GameStateManager.Ins.getPassword)
        // {
        //     if(passwordField.text != GameStateManager.Ins.passwordPC)
        //     {
        //         ErrorDialogue("You", "Hmm, the password is <color=#2567FF>{PASSWORD}</color>... Maybe I typed it wrong.");
        //     }
        //     else
        //     {
        //         // false active locked screen
        //         lockScreen.SetActive(false);
        //         screenLocked.SetActive(false);
        //         // set active all apps
        //         MxWriter.SetActive(false);
        //         mxWriter.SetActive(false);
        //         FileManager.SetActive(false);
        //         fileManager.SetActive(false);
        //     }
        // }
        else
        {
            ErrorDialogue("You", "I don't even know the password. Maybe I should go to the <color=#2567FF>bathroom</color>.");
        }
    }

    public void GoToBathroom()
    {
        if(GameStateManager.Ins.currentState == GameStateManager.Ins.forgotPassword)
        {
            bgBathroom.SetActive(true);
        }
        else 
        {
            ErrorDialogue("You", "I don't want to go bathroom");
        }
    }

    public void BackToRoom()
    {
        bgBathroom.SetActive(false);
    }

    public void SinkClicked()
    {
        if(GameStateManager.Ins.currentState == GameStateManager.Ins.forgotPassword)
        {
            GameStateManager.Ins.SetState(GameStateManager.Ins.bathroom1);
        }
    }

    public void GoOutside()
    {
        if(GameStateManager.Ins.currentState == GameStateManager.Ins.outside1)
        {
            SceneController.Ins.LoadScene("Outside");
        }
        else ErrorDialogue("You", "Not right now <color=#2567FF>{PLAYER_NAME}</color>. finish your job first");
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

    public void FileManagerClicked()
    {
        if(GameStateManager.Ins.currentState == GameStateManager.Ins.draftMissionBegin)
        {
            GameStateManager.Ins.draftMissionBegin.isFileManager = true;
        }
    }

    public void CalendarClicked()
    {
        if(GameStateManager.Ins.currentState == GameStateManager.Ins.draftMissionForgot)
        {
            Dialogue dialogue = GameObject.Find("CalendarDialogue").GetComponent<Dialogue>();
            if(dialogue != null)
            {
                dialogue.TriggerDialogue();
                GameStateManager.Ins.draftMissionForgot.hasClickedCalendar = true;
            }
            // GameStateManager.Ins.SetState(GameStateManager.Ins.mindblink2);
            // next state
            // Dialogue dialogue = GameObject.Find("Calendar").GetComponent<Dialogue>();
            // dialogue.TriggerDialogue();
            // // if(DialogueManager.ins.isDone)
            // // {
            // //     
            // // }
        }
        else 
        {
            ErrorDialogue("You", "What am I supposed to see the calendar?");
        }
    }

    
}
