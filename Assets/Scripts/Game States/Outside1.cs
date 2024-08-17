using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outside1 : IState
{
    private Dialogue dialogue;
    private float countdown;
    private bool isDone;
    public bool isCoffeOutdoor = false;
    public bool isCoffeOutdoor2 = false;
    public bool hasTalkToPublisher = false;
    public bool hasGlitch = false;
    public bool mustShake = false;
    public float glitchDuration = 2f;
    public bool isGlitchActive = false;
    bool isEnd = false;
    public bool cutScene = false;
    public void OnEnter(GameStateManager manager)
    {
        OnActiveScene();
        countdown = 1f;
        isDone = false;
        dialogue = GameObject.Find("Outside1").GetComponent<Dialogue>();
    }

    public void OnExecute(GameStateManager manager)
    {
        countdown -= Time.deltaTime;
        if(countdown <= 0 && !isDone)
        {
            isDone = true;
            dialogue.TriggerDialogue();
        }
        if(!hasGlitch && DialogueManager.ins.isDone)
        {
            if(mustShake) 
            {
                OutsideManager.ins.glitch.SetActive(true);
                Glitch();
                isGlitchActive = true; // Tandai bahwa glitch aktif
            }
        }
        if(isGlitchActive)
        {
            
            glitchDuration -= Time.deltaTime;
            if(glitchDuration <= 0)
            {
                OutsideManager.ins.glitch.SetActive(false);
                isGlitchActive = false;
                hasGlitch = true;
            }
        }
        
        if(hasGlitch && DialogueManager.ins.isDone)
        {   
            
            if(!isEnd)
            {
                isEnd = true;
                Dialogue dialogue = GameObject.Find("cafe-after-glitch").GetComponent<Dialogue>();
                dialogue.TriggerDialogue();
            }
        }
        if(cutScene && DialogueManager.ins.isDone)
        {
            manager.SetState(manager.ending);
        }
    }

    public void OnExit(GameStateManager manager)
    {
        
    }

    void OnActiveScene()
    {
        MainSceneManager.ins.canvasFocused.SetActive(false);
        MainSceneManager.ins.canvasTable.SetActive(false);

        // disable all apps
        MainSceneManager.ins.mxWriter.SetActive(false);
        MainSceneManager.ins.MxWriter.SetActive(false);
        MainSceneManager.ins.fileManager.SetActive(false);
        MainSceneManager.ins.FileManager.SetActive(false);

        MainSceneManager.ins.shutdownScreen.SetActive(true);
        MainSceneManager.ins.screenShutdown.SetActive(true);
    }

    void Glitch()
    {
        SoundEffect.Ins.audioSource.PlayOneShot(SoundEffect.Ins.glitch);
        RectTransform rect = OutsideManager.ins.glitch.GetComponent<RectTransform>();
        Vector3 originalPosition = rect.localPosition;
        OutsideManager.ins.glitch.SetActive(true);
        rect.localPosition = originalPosition + Random.insideUnitSphere * 2f;
        // 
    }
}
