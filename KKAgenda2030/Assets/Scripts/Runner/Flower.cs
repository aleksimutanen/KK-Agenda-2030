using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour {

    public List<Sprite> flowerSprites;
    public List<Sprite> berrySprites;
    SpriteRenderer spriteR;
    int spriteIndex;
    ParticleSystem ps;


    private void Start() {
        spriteR = GetComponentInChildren<SpriteRenderer>();
        spriteIndex = Random.Range(0, flowerSprites.Count);
        spriteR.sprite = flowerSprites[spriteIndex];
        ps = GetComponentInChildren<ParticleSystem>();
    }


    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "Player") {
            RunnerGameManager.instance.HitCollectable(RunnerGameManager.TimerType.GainSmall);
            RunnerGameManager.instance.HitFlower();
            spriteR.sprite = berrySprites[spriteIndex];
            ps.Play();
            //GetComponentInChildren<SpriteRenderer>().sprite = flowerSprites[1];
            // Play some particles and sounds here?
            GetComponent<BoxCollider>().enabled = false;

            //gameObject.SetActive(false);
            //FindObjectOfType<UIManager>().HitFood();
        }
    }

}
