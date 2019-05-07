using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    public SpriteRenderer spriteR;
    public ParticleSystem ps;
    public float particleDelayTime;

    public AudioSource spiderAudio;
    public AudioClip webhit;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "Player") {
            spiderAudio.PlayOneShot(webhit);
            //RunnerGameManager.instance.HitAvoidable(RunnerGameManager.TimerType.LoseSmall);
            RunnerGameManager.instance.LoseLife();
            FindObjectOfType<UIManager>().HitAvoidable();
            //spriteR.enabled = false;
            StartCoroutine("DelayedParticle");
            FindObjectOfType<RunnerController>().hitWeb = true;
        }
    }

    IEnumerator DelayedParticle() {
        yield return new WaitForSeconds(particleDelayTime);
        ps.Play();
        spriteR.enabled = false;
        FindObjectOfType<RunnerController>().hitWeb = false;
    }
}
