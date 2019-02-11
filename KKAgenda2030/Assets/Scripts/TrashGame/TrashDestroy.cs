﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TrashDestroy: MonoBehaviour {

    public List<TrashType> acceptTypes;
    public List<GameObject> Success = new List<GameObject>();
    public int sizeOfList;
    public Animator childAnimator;
    public string eatAnimation;
    public string spitAnimation;

    public void EatTrash(Trash t) {
        childAnimator.Play(eatAnimation);
        Success.Add(t.gameObject);
        sizeOfList = Success.Count;
    }

    public void SpitTrash() {
        childAnimator.Play(spitAnimation);
    }

    //private void Update() {
    //    blinkTimer -= Time.deltaTime;
    //    if (blinkTimer < 0) {
    //        eyeAnimator.Play(blinkAnimation);
    //        blinkTimer = Random.Range(minT, maxT);
    //    }
    //}
    //private void OnTriggerEnter(Collider other) {
    //    var temp = other.GetComponent<Trash>();
    //    if (temp == null) {
    //        return;
    //    }
    //    if (acceptTypes.Contains(temp.kind)) {
    //        childAnimator.Play(eatAnimation);
    //        Success.Add(other.gameObject);
    //        sizeOfList = Success.Count;
    //        other.gameObject.SetActive(false);
    //        //Destroy(other.gameObject);

    //        TrashGameManager.instance.AddedPoints();
    //        TrashGameManager.instance.ResSpawning();
    //        // print("Roskat lajiteltu oikein");

    //    } else { // roska on väärä
    //        TrashGameManager.instance.DeletingPoints();
    //        var dg = other.GetComponent<DragObjects>();
    //        dg.OnDraggingEnd();
    //    }
    //}
}