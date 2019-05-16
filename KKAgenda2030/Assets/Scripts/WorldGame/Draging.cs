using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draging : MonoBehaviour
{
    
    public bool dragging = false;
    Vector3 dist;
    float posX;
    float posY;
    RectScaler rSc;




    public void MouseDown()
    {
        dist = Camera.main.WorldToScreenPoint(transform.position);
        posX = Input.mousePosition.x - dist.x;
        posY = Input.mousePosition.y - dist.y;

        dragging = true;
    }

    public void MouseDrag()
    {
        dragging = true;
        print(dragging);
        Vector3 curPos = new Vector3(Input.mousePosition.x - posX, Input.mousePosition.y - posY, 0);

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(curPos);
        worldPos.z = 0f;
        transform.position = worldPos;

       
    }


    public void MouseUp()
    {
       
        dragging = false;
        print(dragging);
    }
}