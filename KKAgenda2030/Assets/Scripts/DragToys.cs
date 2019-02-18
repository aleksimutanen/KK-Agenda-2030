using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragToys : MonoBehaviour {

    Vector3 dist;
    float posX;
    float posY;
    public ScrollRect sLock;
    GameObject EarthGO;
    bool dragging;
    MenuGameManager mgm;
    Vector3 startPos;

    private void Start() {
        EarthGO = GameObject.Find("Earth");
        sLock = EarthGO.GetComponent<ScrollRect>();
        mgm = FindObjectOfType<MenuGameManager>();
    }

    public void OnMouseDown() {
        //startPos talteen Swappia varten?
        startPos = transform.position;
        dragging = true;
        sLock.vertical = !enabled;
        dist = Camera.main.WorldToScreenPoint(transform.position);
        posX = Input.mousePosition.x - dist.x;
        posY = Input.mousePosition.y - dist.y;
    }

    public void OnMouseDrag() {
        Vector3 curPos = new Vector3(Input.mousePosition.x - posX, Input.mousePosition.y - posY, dist.z);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(curPos);
        transform.position = worldPos;
    }

    public void OnMouseUp() {
        //var mask = LayerMask.GetMask(new string[] { "Ignore Raycast" });
        var colliders = Physics.OverlapSphere(transform.position, 1f, 1 << 2 /*bittishift*/);
        if (colliders.Length == 0) {
            transform.position = startPos;
        } else {
            // TODO valitse lähin collider
            transform.position = colliders[0].transform.position;
        }
        // Toyswap nextinä
        dragging = false;
        sLock.vertical = enabled;


    }

    //private void OnTriggerStay(Collider other) {
    //    if (!dragging) {
    //        transform.position = other.transform.position;
    //        WishToyCheck();
    //        //ToySwap();
    //        //gameobject check mikä lelu on nyt kohdalla vrt wishtoy
    //    }
    //}

    void WishToyCheck() {
        var toyID = GetComponent<ToyID>().ID;
        for (int i = 0; i < mgm.wishToys.Count; i++) {

        }
    }
}


