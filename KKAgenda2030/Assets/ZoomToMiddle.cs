using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoomToMiddle : MonoBehaviour {

    public GameObject topFolder;
    public GameObject showPairsPanel;
    public Transform middlePos;
    Vector3 startPos;
    Vector3 startScale;
    public float transitionSeconds;
    public static bool zoomActive = false;

    public void LerpPairsToMiddle() {
        if (!zoomActive) {
            startPos = transform.position;
            startScale = transform.localScale;
            showPairsPanel.GetComponent<GridLayoutGroup>().enabled = false;
            gameObject.transform.parent = topFolder.transform;
            StartCoroutine(LerpPairs());
        }
    }

    IEnumerator LerpPairs() {
        zoomActive = true;
        var t = 0f;
        while (t < 1) {
            t += Time.deltaTime * (1 / transitionSeconds);
            // smoothstep viritys, ehkä hyvä
            //var newT = t;
            //for (int i = 0; i < 5; i++)
            //    newT = Mathf.SmoothStep(0f, 1f, newT);
            transform.position = Vector3.Lerp(startPos, middlePos.position, t);
            transform.localScale = Vector3.Lerp(startScale, startScale * 1.5f, t);
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        var t2 = 0f;
        while (t2 < 1) {
            t2 += Time.deltaTime * (1 / transitionSeconds);
            transform.position = Vector3.Lerp(middlePos.position, startPos, t2);
            transform.localScale = Vector3.Lerp(startScale * 1.5f, startScale, t2);

            yield return null;
        }
        zoomActive = false;
        gameObject.transform.parent = showPairsPanel.transform;

    }



}
