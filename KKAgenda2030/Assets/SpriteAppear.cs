using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAppear : MonoBehaviour
{
    SpriteRenderer sr;
    float startAlpha = 0f;
    float fadeSpeed = 1.2f;


    void Awake() {
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
        if (startAlpha < 1f) {
            sr.color = new Color(1, 1, 1, startAlpha += Time.deltaTime * fadeSpeed);
        }
    }

    public void alphaFade() {
        while (startAlpha > 0) {
            sr.color = new Color(1, 1, 1, startAlpha -= Time.deltaTime * fadeSpeed);
        }
    }
}
