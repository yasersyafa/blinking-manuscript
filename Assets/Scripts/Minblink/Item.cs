using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum ItemType
{
    Number,
    Place,
    Dessert
}
public class Item : MonoBehaviour
{
    public ItemType itemType;
    public GameObject field;
    public bool isCollect = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnMouseDown()
    {
        if(IsEnabledtoClick())
        {
            return;
        }
        ActivateField();
    }

    public void ActivateField()
    {
        field.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(isCollect)
        {
            Destroy(gameObject);
        }
    }

    bool IsEnabledtoClick()
    {
        // Dapatkan pointer data dari EventSystem
        PointerEventData eventData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        var results = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }

    
}
