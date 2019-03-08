using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDropHandler : MonoBehaviour, IDropHandler
{

   
    public Image[] images = new Image[8];
    public GameObject inventory;


    public void Start()
    {
       
    }

    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        RectTransform invPanel = transform as RectTransform;

        if(!RectTransformUtility.RectangleContainsScreenPoint(invPanel, Input.mousePosition))
        {           
            print("Drop decal");

        }
    }

    public void ImageList(ItemDragHandler item)
    {
        for (int i = 0; i < images.Length; i++)
        {
            if(images[i].name == item.name)
            {
                var go = new GameObject("temp").GetComponent<Image>();
                print("Luodaan objekti");
              
                go.transform.position = images[i].transform.position;

                go.GetComponent<Sprite>();
                go.GetComponent<Image>();
                go.GetComponent<SpriteRenderer>();
            }
        }
    }
}
