using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draging : MonoBehaviour
{
    
    public bool dragging = false;
    Vector3 dist;
    float posX;
    float posY;

    public SpriteRenderer SpriteRenderer
    {
        get;
        private set;
    }
    public BoxCollider BoxCollider
    {
        get;
        private set;
    }

    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        //go.gameObject.GetComponent<Draging>().OnMouseDrag();

        //   go.gameObject.GetComponent<Draging>().dragging = true;
        BoxCollider = GetComponent<BoxCollider>();
    }

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

      /* float distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
       transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));
       */

        print("Triggeri liikkuu");
    }


    public void MouseUp()
    {
        dragging = false;
        print("Triggeri pysähyi");
    }
}