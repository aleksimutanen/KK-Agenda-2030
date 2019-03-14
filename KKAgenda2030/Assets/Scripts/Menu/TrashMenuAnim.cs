using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TrashMenuAnim : MonoBehaviour {

    public Animator midLayerAnim;
    public Animator frontLayerAnim;
    public List<string> treeAnimations;
    float playTimer;
    public float maxT;
    public float minT;


    void Start() {
        playTimer = Random.Range(minT, maxT);
    }

    void Update() {
        playTimer -= Time.deltaTime;
        if (playTimer < 0) {
            midLayerAnim.Play("Menu_MidLayer");
            frontLayerAnim.Play(treeAnimations[Random.Range(0, treeAnimations.Count)]);
            playTimer = Random.Range(minT, maxT);
        }
    }
}
