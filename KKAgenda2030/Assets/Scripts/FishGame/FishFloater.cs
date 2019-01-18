using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishFloater : MonoBehaviour {

    Vector3 originalPos;
    float s;
    float f;
    SpriteRenderer sr;

    public Sprite origFace;
    public Sprite newFace;

    void Start() {
        sr = GetComponentInChildren<SpriteRenderer>();
        originalPos = transform.position;
        s = Random.Range(0.1f, 0.5f);
        f = Random.Range(0.2f, 0.5f);
    }

    void Update() {
        float d = Mathf.Sin(Time.time * Mathf.PI + f * Mathf.PI) * s;
        transform.position = originalPos + Vector3.right * d;
        //                                       /*  +*/ Vector3.forward * Mathf.Sin(Time.time * Mathf.PI + f * Mathf.PI) * s;
        //}
        if (d < 0)
            FlipSpriteXNeg();
        else
            FlipSpriteXPos();
    }

    public void FlipSpriteXNeg() {
        if (!sr.flipX) return;
        else {
            sr.flipX = false;
        }
    }

    public void FlipSpriteXPos() {
        if (sr.flipX) return;
        else {
            sr.flipX = true;
        }
    }

    public void ChangeFace() {
        StartCoroutine("Face");
    }

    IEnumerator Face() {
        //eyesClosed.SetActive(true);
        sr.sprite = newFace;
        yield return new WaitForSeconds(5f);
        sr.sprite = origFace;
        //eyesClosed.SetActive(false);
    }
}
