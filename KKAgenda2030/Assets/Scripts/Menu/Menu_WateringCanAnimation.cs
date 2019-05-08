using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_WateringCanAnimation : MonoBehaviour {

    Animator animator;
    public GameObject growingFlower;
    GrowState gs;
    bool timerStart;
    public float timer;

    void Start() {
        animator = GetComponent<Animator>();
        gs = growingFlower.GetComponent<GrowState>();
       
    }

    private void Update() {
        if (timerStart) {
            timer -= Time.deltaTime;
            if (timer < 0) {
                growingFlower.GetComponent<Animator>().Play("Menu_FlowerShrinken");
                timerStart = false;
                timer = 10f;
                gs.state0 = false;
                gs.state1 = false;
                gs.state2 = false;

            }
        }

    }

    private void OnMouseDown() {
        if (gameObject.name == "WateringCan" && gs.state1) {
            animator.Play("Menu_WateringCan");
            gs.state2 = true;
            growingFlower.GetComponent<Animator>().Play("Menu_Flowergrow3");
            timerStart = true;
        } else if (gameObject.name == "WateringCan" && gs.state0) {
            animator.Play("Menu_WateringCan");
            gs.state1 = true;
            growingFlower.GetComponent<Animator>().Play("Menu_Flowergrow2");
        }
        else if (gameObject.name == "WateringCan" && !gs.state0) {
            animator.Play("Menu_WateringCan");
            gs.state0 = true;
            growingFlower.GetComponent<Animator>().Play("Menu_Flowergrow1");
        }
    }
}
