using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

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
        var colliders = Physics.OverlapSphere(transform.position, 1f/*1 << 2 /*bittishift*/);
        //var dragToys = new List<GameObject>();
        //foreach (var item in colliders) {
        //    if (item.tag == "DragToy") {
        //        dragToys.Add(item.gameObject);
        //    }
        //}

        // Lista löydettyjä muita leluja(collidereita) overlapin alueelta, sama kuin yllä foreach
        var dragToys = new List<GameObject>(colliders.Where(c => c.tag == "DragToy" && c.gameObject != gameObject).Select(c => c.gameObject));
        if (dragToys.Count == 0) {
            transform.position = startPos;
        } else {
            transform.position = dragToys[0].transform.position;
            dragToys[0].transform.position = startPos;
        }

        mgm.WishToyCheck();
        dragging = false;

        sLock.vertical = enabled;
    }
}


