using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draging : MonoBehaviour
{
    
    private bool dragging = false;
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
     

        Vector3 curPos = new Vector3(Input.mousePosition.x - posX, Input.mousePosition.y - posY, dist.z);

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(curPos);
        transform.position = worldPos;

        print("Triggeri liikkuu");
    }


    public void MouseUp()
    {
       
        dragging = false;
        print("Triggeri pysähyi");
    }
}