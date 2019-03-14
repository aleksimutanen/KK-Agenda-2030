using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ItemDragHandler : MonoBehaviour, IDragHandler
{

     public Vector2 startpos;
     public GameObject inventoryPosition;
     public ItemDropHandler IDH;

    public void Awake()
    {
        startpos = new Vector2(transform.position.x, transform.position.y);

    }


    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
       
    }

   public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = Vector3.zero;
        

    }

  /*  public void OnBeginDrag(PointerEventData eventData)
    {
         IDH.ImageList(this);
        
    }/*/

}
