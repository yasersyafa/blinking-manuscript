using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Field : MonoBehaviour
{
    private ChoicePopUp choice;
    TextMeshProUGUI quantityText;
    public bool isFilled;
    // Start is called before the first frame update
    void Start()
    {
        choice = Mindblink2Manager.ins.popUp;
        quantityText = gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        quantityText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenPopUp()
    {
        choice.gameObject.SetActive(true);
        choice.currentField = this;
    }
}
