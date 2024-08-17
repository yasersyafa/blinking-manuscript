using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShutdownAnimation : MonoBehaviour
{
    private TextMeshProUGUI textAnim;
    private bool isStarting;
    // Start is called before the first frame update
    void Start()
    {
        textAnim = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.parent != null)
        {
            if(isStarting)
            {
                
            }
        }
    }
}
