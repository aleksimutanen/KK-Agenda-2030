using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



public class ItemDropHandler : MonoBehaviour, IDropHandler
{

   
    public Image[] images = new Image[8];
    public GameObject inventory;



    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        RectTransform invPanel = transform as RectTransform;

        if(!RectTransformUtility.RectangleContainsScreenPoint(invPanel, Input.mousePosition))
        {           
         //   print("Drop decal");

        }
    }

    public void ImageList(ItemDragHandler item)
    {
        for (int i = 0; i < images.Length; i++)
        {
            if(images[i].name == item.name)
            {
                var go = new GameObject("temp");
                print("Luodaan objekti");
              
                go.transform.position = images[i].transform.position;

               
                go.AddComponent<Image>();
                go.GetComponent<Image>().sprite = images[i].sprite;
                go.layer = 5;
               

               // go.GetComponent<ItemDragHandler>().startpos;
               // go.GetComponent<ItemDragHandler>().IDH = inventory.gameObject;

               

            }
        }
    }
}
