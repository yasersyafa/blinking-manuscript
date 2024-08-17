using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertHandler : MonoBehaviour
{
    public File currentFile;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSendClick()
    {
        if(currentFile != null)
        {
            if(currentFile.isCorrect)
            {
                GameStateManager.Ins.sendDraft.isCorrectDraft = true;
            }
            else MainSceneManager.ins.ErrorDialogue("You", "Hmm... the correct draft is <color=#2567FF>draft-mindblink-2212</color>. Maybe I share the incorrect draft.");
        }
        gameObject.SetActive(false);
    }
}
