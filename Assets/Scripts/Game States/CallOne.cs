using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallOne : IState
{
    private float countdown;
    public bool isDone;
    public bool isShaking;
    private Dialogue dialogue;
    private Vector3 originalPosition;
    private RectTransform rectTransform;
    public void OnEnter(GameStateManager manager)
    {
        dialogue = GameObject.Find("CallOne").GetComponent<Dialogue>();
        rectTransform = MainSceneManager.ins.handphone.GetComponent<RectTransform>();
        countdown = 0.7f;
        isDone = false;
        isShaking = true;
        originalPosition = rectTransform.localPosition;
    }

    public void OnExecute(GameStateManager manager)
    {
        countdown -= Time.deltaTime;
        if(countdown <= 0 && isShaking)
        {
            ShakingPhone(MainSceneManager.ins.handphone);
            if(countdown <= -1.2f && !isDone)
            {
                isDone = true;
                dialogue.TriggerDialogue();
            }
        }

        if(!isShaking)
        {
            SoundEffect.Ins.audioSource.loop = false;
            manager.SetState(manager.publisherOne);
        }
    }

    public void OnExit(GameStateManager manager)
    {
        
    }

    void ShakingPhone(GameObject handphone)
    {
        SoundEffect.Ins.audioSource.PlayOneShot(SoundEffect.Ins.phone);
        SoundEffect.Ins.audioSource.loop = true;
        GameObject effect = handphone.transform.GetChild(1).gameObject;
        effect.SetActive(true);
        // shaking effect
        rectTransform.localPosition = originalPosition + Random.insideUnitSphere * 5f;
    }
}
