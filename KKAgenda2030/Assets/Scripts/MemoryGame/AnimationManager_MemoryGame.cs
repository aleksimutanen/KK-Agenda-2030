using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager_MemoryGame : MonoBehaviour {

    int animalsCount = 0;
    public List<AnimationClip> jigsawState;
    public List<AnimationClip> jigsawRepeatorState;
    public List<GameObject> draggablesAnimator;

    Animator jigsawAnimator;
    public Animator dragAnimalsFolder;
    AnimatorTimer at;

    public GameObject siniGO;
    public GameObject jigsawGO;

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

        if (swinging) {
            swingTimer -= Time.deltaTime;
            if (swingTimer <= 0f) {
                swinging = false;
                jigsawAnimator.Play("JigsawState_middle");
                dragAnimalsFolder.Play("JigsawRepeatState_middle");
                foreach (var item in draggablesAnimator) {
                    item.GetComponent<AnimalDrag_Kaarlo>().PlayIdleAnimation();
                }
                siniGO.GetComponent<AnimatorTimer>().enabled = true;
            }
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
}
