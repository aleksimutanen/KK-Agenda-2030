using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalDrag_Kaarlo : MonoBehaviour {

    Vector3 snapPos;
    public MenuKaarlo_drag mkd;
    public string snapPosName;
    AnimationManager_MemoryGame aMM;

    public void Start() {
        snapPos = GameObject.Find(snapPosName).transform.position;
        aMM = GameObject.Find("AnimationManager").GetComponent<AnimationManager_MemoryGame>();
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
        var colliders = Physics.OverlapSphere(transform.position, 1f);
        bool found = false;
        foreach (var item in colliders) {
            if (item.tag == "Halo")
                found = true;
        }
        if (found) {
            transform.position = snapPos;
            mkd.ResetHalo();
            aMM.AddAnimalCount();
            this.enabled = false;

        } else {
            //gameObject.SetActive(false);
            Destroy(gameObject);
            mkd.ResetState();
        }
    }
}
