using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioList : MonoBehaviour {

    //this script maybe not needed

    AudioSource audioSource;
    public List<AudioClip> audioClips;

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

}
