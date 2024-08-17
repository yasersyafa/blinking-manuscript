using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Ins;
    public IState currentState;
    // varible along game
    public string playerName;
    public string passwordPC;
    // states in game
    public ComputerScene computerScene = new();
    public CallOne callOne = new();
    public PublisherOne publisherOne = new();
    public InputName inputNamePlayer = new();
    public PublisherTwo publisherTwo = new();
    public ForgotPassword forgotPassword = new();
    public Bathroom1 bathroom1 = new();
    public Mindblink1 mindblink1 = new();
    public GetPassword getPassword = new();
    public DraftMissionBegin draftMissionBegin = new();
    public DraftMissionForgot draftMissionForgot = new();
    public Mindblink2 mindblink2 = new();
    public DraftMissionComplete draftMissionComplete = new();
    public SendDraft sendDraft = new();

    // day 2
    public DayTwo day2 = new();
    public ComputerScene2 computerScene2 = new();
    public CallTwo callTwo = new();
    public PublisherThree publisherThree = new();
    public ChangeEndingBegin endingBegin = new();
    public Mindblink3 mindblink3 = new();
    public ChangeEndingCompleted endingCompleted = new();
    public Day3 day3 = new();
    public Outside1 outside1 = new();

    // ending cutscene
    public Ending ending = new();


    [Header("Error Dialogue")]
    public GameObject errorDialogue;

    
    

    private void Awake()
    {
        if(Ins == null)
        {
            Ins = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        currentState = day2;
        currentState?.OnEnter(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState?.OnExecute(this);
    }

    public void SetState(IState newState)
    {
        currentState?.OnExit(this);
        currentState = newState;
        currentState?.OnEnter(this);
    }
}
