using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block2 : MonoBehaviour {
    public ParticleSystem ps;
    public float particleDelayTime;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "Player") {
            //RunnerGameManager.instance.HitAvoidable(RunnerGameManager.TimerType.LoseSmall);
            RunnerGameManager.instance.LoseLife();
            FindObjectOfType<UIManager>().HitAvoidable();
            StartCoroutine("DelayedParticle");
            FindObjectOfType<RunnerController>().hitWeb = true;
        }
    }

    IEnumerator DelayedParticle() {
        yield return new WaitForSeconds(particleDelayTime);
        ps.Play();
        FindObjectOfType<RunnerController>().hitWeb = false;
    }
}
