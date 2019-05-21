using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public GameObject MenuForThegame;
    public GameObject VideoScreen;
    public GameObject creditsTheGame;
    private VideoPlayer videoPlayer;
    [HideInInspector] public AudioSource audioData;
    private string Web =  "https://";
    PageTurner pt;
    GrandManager grandManager;
    public AudioClip[] musicClips = new AudioClip[6];
    public VideoClip[] videoClips = new VideoClip[6];

    void Awake() {
        pt = GameObject.Find("PageTurner").GetComponent<PageTurner>();
        grandManager = GameObject.Find("GrandManager").GetComponent<GrandManager>();
        videoPlayer = VideoScreen.GetComponent<VideoPlayer>();
        audioData = GetComponent<AudioSource>();
        VideoScreen.gameObject.SetActive(false);
        creditsTheGame.gameObject.SetActive(false);
    }

    private void Update() {
        if (VideoScreen.gameObject == true) {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.touchCount > 0 || Input.GetKeyDown(KeyCode.Mouse0)) {
                VideoScreen.gameObject.SetActive(false);
            }
        }
    }

    public void GameOptions() {
        MenuForThegame.gameObject.SetActive(true);
        print("Avataan Options valikko");
        
    }

     
    public void PlayVideo() {
        StopMusic();
        videoPlayer.clip = videoClips[pt.pageIndex - 1];
        // Toggle pagebuttons and bcg music off!
        grandManager.GetComponent<AudioSource>().enabled = false;
        pt.flipButtons();
        //MenuForThegame.gameObject.SetActive(true);
        VideoScreen.gameObject.SetActive(true);
        StartCoroutine(PlaySeaVideo());
        // Screen.SetResolution(720,720,false);
        
    }    


    private void StopVideo() {
        VideoScreen.gameObject.SetActive(false);
        videoPlayer.Stop();
        grandManager.GetComponent<AudioSource>().enabled = true;
        pt.flipButtons();
    }

    public IEnumerator PlaySeaVideo() {
        videoPlayer.Play();       
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => !videoPlayer.isPlaying);
        MenuForThegame.gameObject.SetActive(false);
        StopVideo();
    }

    public void OpenPDF() {
         Application.OpenURL(Web + "www.kierratyskeskus.fi/files/13645/Pikku_simpukka_askarteluohje.pdf");
    }

    public void PlayMusic() {
        audioData.clip = musicClips[pt.pageIndex - 1];
        grandManager.GetComponent<AudioSource>().enabled = false;
        if (audioData.isPlaying) {
            audioData.Stop();
            grandManager.GetComponent<AudioSource>().enabled = true;
        }
        else if (!audioData.isPlaying)
            audioData.Play();
    }

    public void StopMusic() {
        if (audioData.isPlaying) {
            audioData.Stop();
            grandManager.GetComponent<AudioSource>().enabled = true;
        }
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