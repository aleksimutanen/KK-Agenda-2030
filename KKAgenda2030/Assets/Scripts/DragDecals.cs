using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDecals : MonoBehaviour
{
    Vector3  dist;
    Vector3 offset;
    float posX;
    float posY;
    bool dragging;    
    Vector3 startPos;


    public void OnMouseDown()
    {
        //startPos talteen Swappia varten?
        startPos = transform.position;
        dragging = true;
        dist = Camera.main.WorldToScreenPoint(transform.position);
        posX = Input.mousePosition.x - dist.x;
        posY = Input.mousePosition.y - dist.y;
    }

    public void OnMouseDrag()
    {
        Vector3 curPos = new Vector3(Input.mousePosition.x - posX, Input.mousePosition.y - posY, dist.z);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(curPos);
        transform.position = worldPos;
    }


    public void OnMouseUp()
    {

    }
}


