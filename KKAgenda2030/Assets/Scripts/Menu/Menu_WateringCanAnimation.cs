using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_WateringCanAnimation : MonoBehaviour {

    Animator animator;
    public GameObject growingFlower;
    GrowState gs;
    bool timerStart;
    public float timer;

    public AudioSource waterAudio;
    public AudioClip waterCan;
    public AudioClip flowGrow1;
    public AudioClip flowGrow2;
    public AudioClip flowGrow3;
    public AudioClip flowFade;

    bool animatioPlaying = false;

    void Start() {
        animator = GetComponent<Animator>();
        gs = growingFlower.GetComponent<GrowState>();
       
    }

    private void Update() {
        if (timerStart) {
            timer -= Time.deltaTime;
            if (timer < 0) {
                growingFlower.GetComponent<Animator>().Play("Menu_FlowerShrinken");
                waterAudio.PlayOneShot(flowFade);
                timerStart = false;
                timer = 10f;
                gs.state0 = false;
                gs.state1 = false;
                gs.state2 = false;

            }
        }

    }

    private void OnMouseDown() {
        if (!animatioPlaying == true) {
            if (gameObject.name == "WateringCan" && gs.state1) {
                animatioPlaying = true;
                animator.Play("Menu_WateringCan");
                if (!waterAudio.isPlaying) {
                    waterAudio.PlayOneShot(waterCan);
                }
                gs.state2 = true;
                growingFlower.GetComponent<Animator>().Play("Menu_Flowergrow3");
                waterAudio.PlayOneShot(flowGrow3);
                timerStart = true;
            } else if (gameObject.name == "WateringCan" && gs.state0) {
                animator.Play("Menu_WateringCan");
                if (!waterAudio.isPlaying) {
                    waterAudio.PlayOneShot(waterCan);
                }
                gs.state1 = true;
                growingFlower.GetComponent<Animator>().Play("Menu_Flowergrow2");
                waterAudio.PlayOneShot(flowGrow2);
            } else if (gameObject.name == "WateringCan" && !gs.state0) {
                animator.Play("Menu_WateringCan");
                if (!waterAudio.isPlaying) {
                    waterAudio.PlayOneShot(waterCan);
                }
                gs.state0 = true;
                growingFlower.GetComponent<Animator>().Play("Menu_Flowergrow1");
                if (!waterAudio.isPlaying) {
                    waterAudio.PlayOneShot(waterCan);
                }
                waterAudio.PlayOneShot(flowGrow1);
            }
        }
       
        animatioPlaying = false;
    }
}
