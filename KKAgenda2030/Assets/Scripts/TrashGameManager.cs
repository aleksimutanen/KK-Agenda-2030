using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameState
           { Game,
             Restart1,
             Restart2,
             GameMenu };


public class TrashGameManager : MonoBehaviour {

    public static TrashGameManager instance = null;

    public Text statusText;
    public Text scoreText;
    public GameState State;
   
    private int score = 0;
    public Spawner spwn;
    public GSpawners Gspwn;
    public bool levelCompleted = false;
    
   

    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
        {
           // print("TrashGameManager has been found");
        }
        
        //if not, set instance to this
            instance = this;
           // print("TrashGameManager is added to game");
    }

    void Start()
    {
        
        CheckCurrentActiveSceneState();

        spwn = FindObjectOfType<Spawner>();
        Gspwn = FindObjectOfType<GSpawners>();
    }

  
    public void LevelCompleted()
    {
        levelCompleted = false;

        if (spwn.rubbish.Count == 0)
        {
            levelCompleted = true;
                       
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);

            // print(levelCompleted);
        }
    }

    public void UpdatePoints()
    {
        scoreText.text = " " + score;
    }

    public void AddedPoints()
    {
        score += 25;
    }

    public void DeletingPoints()
    {
        score -= 30;
    }

    public void ResSpawning()
    {
        spwn.Spawn();
    }


    private void CheckCurrentActiveSceneState()
    {
        var currentSceneName = SceneManager.GetActiveScene().name;
        
           
        if (State == GameState.Game)
        {
            currentSceneName = "Joni_devscene";
        }

        if (State == GameState.Restart1)
        {
            currentSceneName = "Level-2";
           
        }


        if (State == GameState.Restart2)
        {
            currentSceneName = "Joni_devscene";
            
        }

        // print(currentSceneName);
    }

    
}