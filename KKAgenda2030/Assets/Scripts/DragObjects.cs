using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObjects : MonoBehaviour {

    Vector3 dist;
    float posX;
    float posY;
    float returnSpeed = 20;

    public Vector3 startPos;
    public bool dragging = false;


    void Start() {
        startPos = transform.position;
    }

    void Update() {
        if (dragging) {
            Vector3 curPos = new Vector3(Input.mousePosition.x - posX, Input.mousePosition.y - posY, dist.z);
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(curPos);
            transform.position = worldPos;
        } else {
            var distance = Vector3.Distance(transform.position, startPos);
            if (distance > 0.01f) { // roska liikuu lähtöpistettä kohti
                GetComponent<Collider>().enabled = false;
                var newPos = Vector3.MoveTowards(transform.position, startPos, returnSpeed * Time.deltaTime);
                transform.position = newPos;
                if (Vector3.Distance(transform.position, startPos) <= 0.01f) { // roska on lähtöpisteessä
                    GetComponent<Collider>().enabled = true;
                }
            }
        }
    }

    public void OnDraggingEnd() {
        dragging = false;
    }

    void OnMouseDown() {
        dragging = true;
        dist = Camera.main.WorldToScreenPoint(transform.position);
        posX = Input.mousePosition.x - dist.x;
        posY = Input.mousePosition.y - dist.y;
    }

    void OnMouseUp() {
        OnDraggingEnd();
    }
}
