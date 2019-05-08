using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuKaarlo_drag : MonoBehaviour {

    public Sprite dragSprite;
    public GameObject draggableObject;
    GameObject tempPrefab;
    Animator animator;
    public string emptyAnimation;
    public string defaultAnimation;
    
    public Sprite jigsawHalo;

    Vector3 dist;
    float posX;
    float posY;
    bool dragging = false;
    ScrollRect sLock;
    GameObject kaarloScrollRect;

    Vector3 startPos;
    

    //new animation when drag start, instantiate or toggle on draggable object. If dropped outside goal, switch back.


    void Start() {
        kaarloScrollRect = GameObject.Find("Page5_Kaarlo");
        sLock = kaarloScrollRect.GetComponent<ScrollRect>();
        animator = GetComponent<Animator>();
    }

    public void ResetState() {
        this.enabled = true;
        animator.Play(defaultAnimation);
    }

    private void OnMouseDown() {
        if (!enabled) {
            return;
        }
        sLock.vertical = !enabled;
        animator.Play(emptyAnimation);
        var dragAnimal = Instantiate(draggableObject, transform.position, Quaternion.identity);
        dragAnimal.GetComponent<AnimalDrag_Kaarlo>().mkd = this;
        this.enabled = false;

        //startPos = transform.position;
        //dragging = true;
        //dist = Camera.main.WorldToScreenPoint(transform.position);
        //posX = Input.mousePosition.x - dist.x;
        //posY = Input.mousePosition.y - dist.y;
    }

    //public void OnMouseDrag() {
    //    Vector3 curPos = new Vector3(Input.mousePosition.x - posX, Input.mousePosition.y - posY, dist.z);
    //    Vector3 worldPos = Camera.main.ScreenToWorldPoint(curPos);
    //    transform.position = worldPos;
    //}

    //public void OnMouseUp() {
    //    var colliders = Physics.OverlapSphere(transform.position, 1f);
    //    var dragToys = new List<GameObject>();
    //    foreach (var item in colliders) {
    //        if (item.tag == "Halo") {
    //            dragToys.Add(item.gameObject);
    //        }
    //    }
    //    if (dragToys.Count == 0) {
    //        transform.position = startPos;
    //        animator.Play(defaultAnimation);
    //        // play default animation
    //    } else {
    //        transform.position = snapPos;
    //        // this items collider off, so item couldnt be moved anymore?
    //    }
    //    sLock.vertical = enabled;
    //    dragging = false;
    //}

}
