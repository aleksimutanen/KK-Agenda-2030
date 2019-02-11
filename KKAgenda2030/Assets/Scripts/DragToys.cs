using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragToys : MonoBehaviour
{

    Vector3 dist;
    float posX;
    float posY;
    public ScrollRect sLock;

    private void Awake()
    {
        sLock = FindObjectOfType<ScrollRect>();
    }

    public void OnMouseDown()
    {
        sLock.vertical = !enabled;
        dist = Camera.main.WorldToScreenPoint(transform.position);
        posX = Input.mousePosition.x - dist.x;
        posY = Input.mousePosition.y - dist.y;
    }

    public void OnMouseDrag()
    {
        Vector3 curPos = new Vector3(Input.mousePosition.x - posX, Input.mousePosition.y - posY, dist.z);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(curPos);
        transform.position = worldPos;
        //print("Siirtyyy!");

      
    }

    public void OnMouseUp()
    {
        sLock.vertical = enabled;
    }



}


