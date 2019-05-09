using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager_MemoryGame : MonoBehaviour {

    public int animalsCount = 0;
    public List<AnimationClip> jigsawState;
    public List<AnimationClip> jigsawRepeatorState;
    Animator jigsawAnimator;
    public Animator dragAnimalsFolder;
    AnimatorTimer at;
    public GameObject jigsawGO;
    


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
    }

    public void SetJigsawState() {
        jigsawAnimator.Play(jigsawState[animalsCount].name);
    }

    public void AddAnimalCount() {
        animalsCount++;
        jigsawAnimator.Play(jigsawState[animalsCount].name);
        dragAnimalsFolder.Play(jigsawRepeatorState[animalsCount-1].name);
    }
}
