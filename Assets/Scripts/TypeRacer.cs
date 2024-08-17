using System.Collections;
using TMPro;
using UnityEngine;

public class TextChecker : MonoBehaviour
{
    public string paragraph = "The shadows of the past lingered, casting doubts on everything he thought he knew. In the silence that followed, memories began to resurface, fragments of a truth he had long tried to forget. As the clock struck midnight, he realized that the line between reality and delusion had already started to blur.".ToLower();
    public TextMeshProUGUI textGenerator;
    private string remainingWord = string.Empty;
    private string currentWord = string.Empty;
    bool isStart = false;
    float timer;
    
    // Start is called before the first frame update
    void Start()
    {
        SetCurrentWord();
    }

    void SetCurrentWord()
    {
        currentWord = paragraph.ToLower(); // use the paragraph directly
        SetRemainingWord(currentWord);
    }

    void SetRemainingWord(string letter)
    {
        remainingWord = letter;
        textGenerator.text = remainingWord;
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    void CheckInput()
    {
        if(Input.anyKeyDown)
        {
            string keyPressed = Input.inputString;
            if(keyPressed.Length == 1) EnterLetter(keyPressed);
            isStart = true;
        }
        if(isStart)
        {
            timer += Time.deltaTime;   
        }
    }

    void EnterLetter(string typedLetter)
    {
        if(IsCorrectLetter(typedLetter))
        {            
            RemoveRemainingWord();
            if(IsWordComplete())
            {
                // isStart = false;
                // ResultGame();
                // Debug.Log(timer);
                SetCurrentWord();
            }
        } 
    }

    bool IsCorrectLetter(string letter) 
    {
        return remainingWord.IndexOf(letter) == 0;
    }

    void RemoveRemainingWord()
    {
        string newString = remainingWord.Remove(0, 1);
        SetRemainingWord(newString);
    }

    public void RestartType()
    {
        SetCurrentWord();
    }

    bool IsWordComplete()
    {
        return remainingWord.Length == 0;
    }

    void ResultGame()
    {

    }
}
