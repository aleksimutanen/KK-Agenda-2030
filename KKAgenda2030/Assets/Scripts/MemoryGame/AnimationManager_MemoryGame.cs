using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager_MemoryGame : MonoBehaviour {

    int animalsCount = 0;
    public List<AnimationClip> jigsawState;
    public List<AnimationClip> jigsawRepeatorState;
    public List<GameObject> draggablesAnimator;

    public Animator jigsawAnimator;
    public Animator dragAnimalsFolder;
    AnimatorTimer at;

    public GameObject siniGO;
    public GameObject jigsawGO;
    public GameObject replayButton;

    float swingTimer = 6f;
    bool swinging;
    


    private void Start() {
        jigsawAnimator = jigsawGO.GetComponent<Animator>();
        at = jigsawAnimator.GetComponentInParent<AnimatorTimer>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (animalsCount == 4) {
                jigsawAnimator.Play("Jigsaw_wholeSwing");
            } else {
                animalsCount++;
                var s = at.animationPool[0];
                at.animationPool[0] = s.Substring(0, s.Length - 1) + (s[s.Length - 1] - '0' + 1);
                SetJigsawState();
            }
        }

        //if (swinging) {
        //    swingTimer -= Time.deltaTime;
        //    if (swingTimer <= 0f) {
        //        swinging = false;
        //        jigsawAnimator.Play("JigsawState_middle");
        //        dragAnimalsFolder.Play("JigsawRepeatState_middle");
        //        foreach (var item in draggablesAnimator) {
        //            item.GetComponent<AnimalDrag_Kaarlo>().PlayIdleAnimation();
        //        }
        //        siniGO.GetComponent<AnimatorTimer>().enabled = true;
        //    }
        //}

        if (swinging) {
           foreach (var item in draggablesAnimator) {
           item.GetComponent<Animator>().Play("New State");
           }
            // toggle resetbutton on
            replayButton.SetActive(true);
        }
    }

    public void AddAnimalCount() {
        if (animalsCount == 0) {
            StopSiniAnimation();
        }
        animalsCount++;
        jigsawAnimator.Play(jigsawState[animalsCount].name);
        dragAnimalsFolder.Play(jigsawRepeatorState[animalsCount-1].name);
        // start timer and when times up, stop the animations in the middle?
        if (animalsCount == 4) {
            swinging = true;
        }
    }

    public void SetJigsawState() {
        jigsawAnimator.Play(jigsawState[animalsCount].name);
    }

    public void StopSiniAnimation() {
        siniGO.GetComponent<AnimatorTimer>().enabled = false;
    }

    public void ResetMinigame() {
        animalsCount = 0;
        SetJigsawState();
        dragAnimalsFolder.Play("New State");
        foreach (var item in draggablesAnimator) {
        Destroy(item);
        }
        draggablesAnimator.Clear();
        var temp = GameObject.FindObjectsOfType<MenuKaarlo_drag>();
        foreach (var item in temp) {
            item.ResetState();
        }
        siniGO.GetComponent<AnimatorTimer>().enabled = true;
        swinging = false;
        replayButton.SetActive(false);
    }
}
