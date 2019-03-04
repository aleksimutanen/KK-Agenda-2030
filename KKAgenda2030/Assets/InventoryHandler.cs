using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryHandler : MonoBehaviour
{

    public List<Image> inventoryObjects = new List<Image>();

    // Start is called before the first frame update
    void Start()
    {
        
    }


    void OnMouseDown()
    {
        for (int i = 0; i < inventoryObjects.Count; i++)
        {
            var clone = inventoryObjects[i].transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
