﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameState
           { Level1,
             Level2,
             Level3,
             GameMenu };


public class TrashGameManager : MonoBehaviour {

    public static TrashGameManager instance = null;

    public Text statusText;
    public Text scoreText;
    public GameState State { get; private set; }
   
    private int score = 0;
    public Spawner spwn;
    public bool levelCompleted = false;
    
   

    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
        {
            print("TrashGameManager has been found");
        }
        
        //if not, set instance to this
            instance = this;
            print("TrashGameManager is added to game");
    }

    void Start()
    {
        
        CheckCurrentActiveScene();
        spwn = FindObjectOfType<Spawner>();
    }


    public void LevelCompleted()
    {
        levelCompleted = false;

        if (spwn.rubbish.Count.Equals(0))
        {
            levelCompleted = true;
            print(levelCompleted);
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


    private void CheckCurrentActiveScene()
    {
        var currentSceneName = SceneManager.GetActiveScene().name;
        
        if (currentSceneName == "Joni_devscene")
        {                  
            State = GameState.Level1;
        }

        //if (currentSceneName == "Joni_devscene")
        //{
        //    State = GameState.Level2;
        //}


        //if (currentSceneName == "Joni_devscene")
        //{
        //    State = GameState.Level3;
        //}

        //if (currentSceneName == "MenuScene")
        //{
        //    State = GameState.GameMenu;
        //}

    }


}