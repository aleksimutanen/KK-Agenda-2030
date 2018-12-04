using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public GameObject MenuForThegame;
    public GameObject ThisSite;
    public GameObject VideoScreen;
    public GameObject creditsTheGame;
    private VideoPlayer seaVideoPlayer;
    private AudioSource audioData;
    private string Web =  "https://";
    

    void Awake()
    {
        seaVideoPlayer = GetComponent<VideoPlayer>();
        audioData = GetComponent<AudioSource>();
       
        VideoScreen.gameObject.SetActive(false);
    }

  

    public void GameOptions()
    {
        MenuForThegame.gameObject.SetActive(true);
        
    }

     
    public void PlayVideo()
    {
        ThisSite.gameObject.SetActive(false);
        VideoScreen.gameObject.SetActive(true);

        StartCoroutine(PlaySeaVideo());

        // Screen.SetResolution(720,720,false);
        print("Video alkaa");
    }    


    private void StopVideo()
    {
        VideoScreen.gameObject.SetActive(false);
        ThisSite.gameObject.SetActive(true);
        seaVideoPlayer.Stop();
        print("Video pysähtyy");
    }

    public IEnumerator PlaySeaVideo()
    {
        seaVideoPlayer.Play();

        yield return new WaitForSeconds(1f);

        yield return new WaitUntil(() => !seaVideoPlayer.isPlaying);

        StopVideo();
    }

    public void OpenPDF()
    {
         Application.OpenURL(Web + "www.kierratyskeskus.fi/files/13645/Pikku_simpukka_askarteluohje.pdf");
    }

    public void PlayMusic()
    {
        audioData.Play();
        print("Soita musiikia");
    }

    public void Credits()
    {
        MenuForThegame.gameObject.SetActive(false);
        creditsTheGame.gameObject.SetActive(true);
    }

    public void Back()
    {
        MenuForThegame.gameObject.SetActive(false);
    }

    public void BackToOptions()
    {
        creditsTheGame.gameObject.SetActive(false);
        MenuForThegame.gameObject.SetActive(true);
    }


    public void QuitGame()
    {      
        Application.Quit();
    }

}