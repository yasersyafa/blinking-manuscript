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
        if (GameStateManager.Ins.currentState is IShutdownHandler handler)
            handler.OnShutdownClicked(GameStateManager.Ins);
        else
            ErrorDialogue("You", "There is something I want to do it right now");
    }

    public void EnterPassword()
    {
        if (GameStateManager.Ins.currentState != GameStateManager.Ins.getPassword)
        {
            ErrorDialogue("You", "I don't even know the password.");
            return;
        }

        if (string.IsNullOrEmpty(passwordField.text) ||
            passwordField.text != GameStateManager.Ins.passwordPC)
        {
            ErrorDialogue("You", "Hmm, the password is <color=#2567FF>{PASSWORD}</color>... Maybe I typed it wrong.");
            return;
        }

        lockScreen.SetActive(false);
        screenLocked.SetActive(false);
        MxWriter.SetActive(false);
        mxWriter.SetActive(false);
        FileManager.SetActive(false);
        fileManager.SetActive(false);
        GameStateManager.Ins.SetState(GameStateManager.Ins.draftMissionBegin);
    }

    public void StopShakingPhone()
    {
        SoundEffect.Ins.audioSource.Stop();
        if (GameStateManager.Ins.currentState is IPhoneHandler handler)
            handler.OnPhoneStopClicked(GameStateManager.Ins);
    }

    public void GoToBathroom()
    {
        if (GameStateManager.Ins.currentState is IBathroomHandler handler)
            handler.OnBathroomClicked(GameStateManager.Ins);
        else
            ErrorDialogue("You", "I don't want to go bathroom");
    }

    public void BackToRoom()
    {
        bgBathroom.SetActive(false);
    }

    public void SinkClicked()
    {
        if (GameStateManager.Ins.currentState is ISinkHandler handler)
            handler.OnSinkClicked(GameStateManager.Ins);
    }

    public void FileManagerClicked()
    {
        if (GameStateManager.Ins.currentState is IFileManagerHandler handler)
            handler.OnFileManagerClicked(GameStateManager.Ins);
    }

    public void GoOutside()
    {
        if (GameStateManager.Ins.currentState == GameStateManager.Ins.outside1)
            SceneController.Ins.LoadScene("Outside");
        else
            ErrorDialogue("You", "Not right now. Finish your job first.");
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

    public void CalendarClicked()
    {
        if (GameStateManager.Ins.currentState is ICalendarHandler handler)
            handler.OnCalendarClicked(GameStateManager.Ins);
        else
            ErrorDialogue("You", "What am I supposed to see the calendar?");
    }

    public void InputPlayerName()
    {
        if (GameStateManager.Ins.currentState is IState &&
            GameStateManager.Ins.currentState == GameStateManager.Ins.inputNamePlayer)
        {
            GameStateManager.Ins.playerName = inputField.text;
            GameStateManager.Ins.inputNamePlayer.isFilled = true;
        }
    }

    
}
