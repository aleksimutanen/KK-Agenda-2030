using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    public SpriteRenderer spriteR;
    public ParticleSystem ps;


    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "Player") {
            //RunnerGameManager.instance.HitAvoidable(RunnerGameManager.TimerType.LoseSmall);
            RunnerGameManager.instance.LoseLife();
            FindObjectOfType<UIManager>().HitAvoidable();
            spriteR.enabled = false;
            ps.Play();
        }
    }
}
