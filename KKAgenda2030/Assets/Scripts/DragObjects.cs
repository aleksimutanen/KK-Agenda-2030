using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObjects : MonoBehaviour {

    Spawner spwn;
    Vector3 dist;
    float posX;
    float posY;
    private float distance = 3f;
    private float lerpTime = 5;
    private float currentLerpTime = 0;
    private Vector3 startPos;
    private Vector3 endPos;
    public bool dragging = false;


    void Start()
    {
        spwn = FindObjectOfType<Spawner>();
        startPos = transform.position;
        endPos = transform.position + Vector3.down * distance;
    }

    void OnDraggingEnd()
    {
        dragging = false;
        if (dragging == false)
        {
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime >= lerpTime)
            {
                currentLerpTime = lerpTime;
            }

            float Perc = currentLerpTime / lerpTime;
            transform.position = Vector3.Lerp(startPos, endPos, Perc);
        }
    }


    public void OnMouseDown()
    {

        dist = Camera.main.WorldToScreenPoint(transform.position);
        posX = Input.mousePosition.x - dist.x;
        posY = Input.mousePosition.y - dist.y;
    }

    public void OnMouseDrag()
    {        
            dragging = true;
            Vector3 curPos = new Vector3(Input.mousePosition.x - posX, Input.mousePosition.y - posY, dist.z);
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(curPos);
            transform.position = worldPos;
            // print("Siirtyyy!");
       
    }

    public void OnMouseUp()
    {
        print("Draggaus loppu");

        dragging = false;

        if (dragging == false)
        {
            OnDraggingEnd();
            print(dragging);
            print("Voidaan palata");
        }
    }




}
