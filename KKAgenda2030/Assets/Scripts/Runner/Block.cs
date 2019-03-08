using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "Player") {
            RunnerGameManager.instance.HitAvoidable(RunnerGameManager.TimerType.LoseSmall);
            RunnerGameManager.instance.LoseLife();
            FindObjectOfType<UIManager>().HitAvoidable();
            gameObject.SetActive(false);
        }
    }
}
