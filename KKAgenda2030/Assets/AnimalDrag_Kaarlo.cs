using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalDrag_Kaarlo : MonoBehaviour {

    Vector3 snapPos;
    public MenuKaarlo_drag mkd;
    public string snapPosName;
    //public GameObject parentFolder;


    public void Start() {
        snapPos = GameObject.Find(snapPosName).transform.position;
        //transform.parent = parentFolder.transform;

    }

    void Update() {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPos.z = 0f;
        transform.position = worldPos;

        if (Input.GetKeyUp(KeyCode.Mouse0)) { // TODO: phoneTouch
            OnEndDrag();
        }
    }

    public void OnEndDrag() {
        print("onmouseup NOW");
        var colliders = Physics.OverlapSphere(transform.position, 1f);
        bool found = false;
        foreach (var item in colliders) {
            if (item.tag == "Decal")
                found = true;
        }
        if (found) {
            print("decal hit");
            transform.position = snapPos;
            this.enabled = false;
        } else {
            gameObject.SetActive(false);
            mkd.ResetState();
        }
    }
}
