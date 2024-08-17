using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveApp : MonoBehaviour
{
    public GameObject interactiveApp, disabledApp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetActiveApp()
    {
        interactiveApp.SetActive(true);
        disabledApp.SetActive(true);
        // set as last sibling
        interactiveApp.transform.SetAsLastSibling();
        disabledApp.transform.SetAsLastSibling();
    }
}
