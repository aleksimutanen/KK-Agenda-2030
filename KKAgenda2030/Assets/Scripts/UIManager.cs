using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    PageTurner pt;

    public Button pause;
    bool paused;

	void Start () {
        //paused = true;
        pt = FindObjectOfType<PageTurner>();
	}
	
	void Update () {
		
	}

    public void LaunchOceanGame() {
        GrandManager.instance.LaunchOceanGame();
    }

    public void NextPage() {
        pt.NextPage();
    }

    public void PreviousPage() {
        pt.PreviousPage();
    }

    public void PauseButton() {
        if (paused) {
            //Anim("PauseButtonAnimationOut");
            StartCoroutine(Anim("PauseButtonAnimationOut"));
            paused = false;
        } else {
            //Anim("PauseButtonAnimation");
            StartCoroutine(Anim("PauseButtonAnimation"));
            paused = true;
        }
        //bool paused = GrandManager.instance.Pause();
        //if (paused)
        //    pause.GetComponent<Animator>().Play("PauseButtonAnimation");
        //else
        //    pause.GetComponent<Animator>().Play("PauseButtonAnimationOut");
        //print("transition");
        //StartCoroutine("Anim");
    }

    IEnumerator Anim(string animName) {
        if (GrandManager.instance.paused) {
            GrandManager.instance.Pause();
            pause.GetComponent<Animator>().Play(animName);
            yield return null;
        } else {
            pause.GetComponent<Animator>().Play(animName);
            yield return new WaitForSeconds(1f);
            bool paused = GrandManager.instance.Pause();
            yield return null;
        }
    }
}
