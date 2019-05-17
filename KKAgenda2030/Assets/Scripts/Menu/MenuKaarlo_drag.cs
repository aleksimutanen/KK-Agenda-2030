using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuKaarlo_drag : MonoBehaviour {

    // this script is on every draggable animal

    public AnimationManager_MemoryGame aMM;

    public Sprite dragSprite;
    public GameObject draggableObject;
    GameObject dragAnimal;
    Animator animator;
    public string emptyAnimation;
    public string defaultAnimation;
    public GameObject jigsawHalo;
    public GameObject draggablesFolder;

    ScrollRect sLock;
    GameObject kaarloScrollRect;

    AnimatorTimer at;

    void Awake() {
        kaarloScrollRect = GameObject.Find("Page6_Kaarlo");
        sLock = kaarloScrollRect.GetComponent<ScrollRect>();
        animator = GetComponent<Animator>();
    }

    //void Start() {
    //    kaarloScrollRect = GameObject.Find("Page6_Kaarlo");
    //    sLock = kaarloScrollRect.GetComponent<ScrollRect>();
    //    animator = GetComponent<Animator>();
    //}

    public void ResetState() {
        if (at) {
            at.enabled = true;
        }
        this.enabled = true;
        animator.Play(defaultAnimation);
        sLock.vertical = enabled;
        gameObject.GetComponent<BoxCollider>().enabled = true;
        jigsawHalo.SetActive(false);
    }

    public void ResetHalo() {
        this.enabled = true;
        sLock.vertical = enabled;
        jigsawHalo.SetActive(false);
        gameObject.GetComponent<BoxCollider>().enabled = false;
        aMM.draggablesAnimator.Add(dragAnimal);
    }

    private void OnMouseDown() {
        at = GetComponent<AnimatorTimer>();
        if (at) {
            at.enabled = false;
        }

        if (!enabled) {
            return;
        }
        jigsawHalo.SetActive(true);
        sLock.vertical = !enabled;
        animator.Play(emptyAnimation);
        dragAnimal = Instantiate(draggableObject, transform.position, Quaternion.identity);
        dragAnimal.GetComponent<Transform>().parent = draggablesFolder.transform;
        dragAnimal.GetComponent<AnimalDrag_Kaarlo>().mkd = this;
        this.enabled = false;

    }

    
}
