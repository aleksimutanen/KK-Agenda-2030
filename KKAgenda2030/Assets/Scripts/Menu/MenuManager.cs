using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    public GameObject MenuForThegame;
    public GameObject videoScreen;
    public GameObject creditsTheGame;
    public VideoPlayer vp;
    [HideInInspector] public AudioSource audioData;
    private string Web =  "https://";
    PageTurner pt;
    GrandManager grandManager;
    public AudioClip[] musicClips = new AudioClip[6];
    public VideoClip[] videoClips = new VideoClip[6];
    public VideoClip[] DIYClips = new VideoClip[6];
    public GameObject[] audioSourceGO = new GameObject[6];
    public GameObject[] musicButtonParticles = new GameObject[6];


    void Awake() {
        pt = GameObject.Find("PageTurner").GetComponent<PageTurner>();
        grandManager = GameObject.Find("GrandManager").GetComponent<GrandManager>();
        audioData = GetComponent<AudioSource>();
        creditsTheGame.gameObject.SetActive(false);
    }

    private void Update() {
        if (vp.isPlaying && Input.GetKeyDown(KeyCode.Mouse0)) {
            vp.Stop();
        }
    }

    public void TestPlay(VideoClip clip) {
        StopMusic();

        vp.clip = clip;
        audioSourceGO[pt.pageIndex - 1].GetComponent<AudioSource>().enabled = false;
        grandManager.GetComponent<AudioSource>().enabled = false;
        pt.flipButtons();
        StartCoroutine(PlayVideoClip());
    }

    public void GameOptions() {
        MenuForThegame.gameObject.SetActive(true);
        print("Avataan Options valikko");
        
    }

     
    //public void PlayVideo() {
    //    StopMusic();
    //    videoPlayer.clip = videoClips[pt.pageIndex - 1];

    //    // Toggle pagebuttons and bcg music off!
    //    audioSourceGO[pt.pageIndex - 1].GetComponent<AudioSource>().enabled = false;
    //    grandManager.GetComponent<AudioSource>().enabled = false;
    //    pt.flipButtons();
    //    StartCoroutine(PlayVideoClip());
    //    // Screen.SetResolution(720,720,false);
    //}

    public void PlayDIYVideo(VideoClip clip) {
        StopMusic();
        vp.clip = clip;
        // Toggle pagebuttons and bcg music off!
        grandManager.GetComponent<AudioSource>().enabled = false;
        audioSourceGO[pt.pageIndex - 1].GetComponent<AudioSource>().enabled = false;
        pt.flipButtons();
        StartCoroutine(PlayVideoClip());
        // Screen.SetResolution(720,720,false);

    }


    private void StopVideo() {
        pt.flipButtons();
        grandManager.GetComponent<AudioSource>().enabled = true;
        audioSourceGO[pt.pageIndex - 1].GetComponent<AudioSource>().enabled = true;
        vp.Stop();
    }

    public IEnumerator PlayVideoClip() {
        vp.Play();
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => !vp.isPlaying);
        StopVideo();
    }

    public void OpenPDF() {
         Application.OpenURL(Web + "www.kierratyskeskus.fi/files/13645/Pikku_simpukka_askarteluohje.pdf");
    }

    public void PlayMusic() {
        audioData.clip = musicClips[pt.pageIndex - 1];
        grandManager.GetComponent<AudioSource>().enabled = false;
        audioSourceGO[pt.pageIndex - 1].GetComponent<AudioSource>().enabled = false;
        ToggleParticles(musicButtonParticles[pt.pageIndex - 1].gameObject);
        if (audioData.isPlaying) {
            audioData.Stop();
            grandManager.GetComponent<AudioSource>().enabled = true;
            audioSourceGO[pt.pageIndex - 1].GetComponent<AudioSource>().enabled = true;
            //ToggleParticles(musicButtonParticles[pt.pageIndex]);
        } else if (!audioData.isPlaying)
            audioData.Play();
    }

    public void StopMusic() {
        
        if (audioData.isPlaying) {
            audioData.Stop();
            grandManager.GetComponent<AudioSource>().enabled = true;
            audioSourceGO[pt.pageIndex - 1].GetComponent<AudioSource>().enabled = true;
        }
        if (pt.pageIndex > 0) {
            if (musicButtonParticles[pt.pageIndex - 1].gameObject.activeSelf) {
                musicButtonParticles[pt.pageIndex - 1].gameObject.SetActive(false);
            }
        }
    }

    void ToggleParticles(GameObject ps) {
        ps.SetActive(!ps.activeSelf);
    }

    public void Credits() {
        MenuForThegame.gameObject.SetActive(false);
        creditsTheGame.gameObject.SetActive(true);
        print("Avataan Credits ikkuna");
    }

    public void ContinueGame() {
        MenuForThegame.gameObject.SetActive(false);
        print("Jatketaan peliä");
    }

    public void Back() {
        creditsTheGame.gameObject.SetActive(false);
        MenuForThegame.gameObject.SetActive(true);
    }

    public void Options() {
        MenuForThegame.gameObject.SetActive(true);
    }


    public void QuitGame() {      
        Application.Quit();
        print("Poistutaan pelistä");
    }

}