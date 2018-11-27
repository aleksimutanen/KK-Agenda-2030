using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public GameObject MenuForThegame;
    public GameObject creditsTheGame;

    public void NextLevel()
    {
        SceneManager.LoadScene("Aleksi_devscene");
    }

    public void GameOptions()
    {
        MenuForThegame.gameObject.SetActive(true);
        
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