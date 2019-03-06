using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInteractive : MonoBehaviour {

    // FISHGAME
    public Camera menuCam;
    public GameObject bubblePopper;
    public string siniSimpukkaAnim;
    public string pikeAnim;
    public string roachAnim;
    public Transform siniSimpukka;
    public Transform pike;
    public Transform roach;

    //TRASHGAME (Not in use atm)
    string lennu_show;
    GameObject lennuSquirt;



	void Update () {
        if (Input.GetKeyDown(KeyCode.Mouse0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)) {
            Vector3 screenPos = menuCam.ScreenToWorldPoint(Input.mousePosition);
            screenPos.z = 0f;
            bubblePopper.SetActive(true);
            bubblePopper.transform.position = screenPos;
            Invoke("StopTouch", 0.25f);
        }
    }

    public void StopTouch() {
        bubblePopper.SetActive(false);
    }

    public void PlaySiniSimpukkaAnim() {
        siniSimpukka.GetComponent<Animator>().Play(siniSimpukkaAnim);
    }

    public void PlayPikeAnim() {
        pike.GetComponent<Animator>().Play(pikeAnim);
    }

    public void PlayRoachAnim() {
        roach.GetComponent<Animator>().Play(roachAnim);
    }


    // TRASHGAME
    public void PlayLennuAnim() {
        lennuSquirt.GetComponent<Animator>().Play(lennu_show);
    }
}
