using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInteractive : MonoBehaviour {

    public string siniSimpukkaAnim;
    public string pikeAnim;

    public Transform siniSimpukka;
    public Transform pike;

	void Start () {
	}
	
    public void PlaySiniSimpukkaAnim() {
        siniSimpukka.GetComponent<Animator>().Play(siniSimpukkaAnim);
    }

    public void PlayPikeAnim() {
        pike.GetComponent<Animator>().Play(pikeAnim);
    }
}
