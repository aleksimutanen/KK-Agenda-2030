using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoomToMiddle : MonoBehaviour {

    public Transform middlePos;
    public GameObject ShowPairsPanel;
    Vector3 startPos;
    public float transitionSeconds;

    public void LerpPairsToMiddle() {
        startPos = transform.position;
        ShowPairsPanel.GetComponent<GridLayoutGroup>().enabled = false;
        StartCoroutine(LerpPairs());
    }

    IEnumerator LerpPairs() {
        var t = 0f;
        while (t < 1) {
            t += Time.deltaTime * (1 / transitionSeconds);
            // smoothstep viritys, ehkä hyvä
            var newT = t;
            for (int i = 0; i < 10; i++)
                newT = Mathf.SmoothStep(0f, 1f, newT);

            transform.position = Vector3.Lerp(startPos, middlePos.position, newT);
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        var t2 = 0f;
        while (t2 < 1) {
            t2 += Time.deltaTime * (1 / transitionSeconds);
            transform.position = Vector3.Lerp(middlePos.position, startPos, t2);
            yield return null;
        }

    }



}
