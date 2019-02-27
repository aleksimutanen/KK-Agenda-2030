using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "Player") {
            RunnerGameManager.instance.HitCollectable(RunnerGameManager.TimerType.GainSmall);
            RunnerGameManager.instance.HitFlower();
            gameObject.SetActive(false);
            //FindObjectOfType<UIManager>().HitFood();
        }
    }

}
