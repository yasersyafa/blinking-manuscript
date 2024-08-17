using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChoicePopUp : MonoBehaviour
{
    public Field currentField;
    [HideInInspector] public int choice;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetChoice(int point)
    {
        choice = point;
        SetText(currentField);
        gameObject.SetActive(false);
    }

    void SetText(Field field)
    {
        GameObject item = field.gameObject;
        TextMeshProUGUI textQuantity = item.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        textQuantity.text = choice.ToString();
        field.isFilled = true;
        Mindblink2Manager.ins.CheckResult();
    }
}
