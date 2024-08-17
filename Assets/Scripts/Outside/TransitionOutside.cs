using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionOutside : MonoBehaviour
{
    GameObject active, deactivate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetGameObject(GameObject to, GameObject from)
    {
        active = to;
        deactivate = from;
    }

    public void EnabledObject()
    {
        deactivate.SetActive(false);
        active.SetActive(true);
    }
}
