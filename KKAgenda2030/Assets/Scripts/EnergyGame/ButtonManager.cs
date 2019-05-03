using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour {
    public enum scenes { livingRoom, Yard, Kitchen }

    public scenes currentScene;

    public int sceneIndex;

    public List<Toggle> livingRoomToggles;
    public List<Image> livingRoomOnImages;
    public List<Image> livingRoomOffImages;

    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void changeImage() {
        if (currentScene == scenes.livingRoom) {
            foreach(Toggle toggle in livingRoomToggles) {

            }
        }
    }
}
