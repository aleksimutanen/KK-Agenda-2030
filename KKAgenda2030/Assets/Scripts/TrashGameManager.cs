using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameState
           { Level1,
             Level2,
             Level3,
             MainMenu };


public class TrashGameManager : MonoBehaviour {

    public static TrashGameManager instance = null;

    public Text statusText;
    public Text scoreText;
    public GameState state;

    private int score = 0;
    public TrashDestroy[] TrashCans;
    public bool levelIsCompleted;
    public int listIsFull;

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
        TrashCans = FindObjectsOfType<TrashDestroy>();
        listIsFull = GetComponent<TrashDestroy>().maxOfList;
    }


    public bool AllTrashcansFull()
    {
            levelIsCompleted = false;

        foreach (var can in TrashCans)
        {
            if (can.isFull)
            {
                print("Taso on Suoritettu");
                levelIsCompleted = true;
                
            }

            if (levelIsCompleted == true)
            {
                SceneManager.LoadScene("");
            }
        }
        return levelIsCompleted;
    }

    public void UpdatePoints()
    {
        scoreText.text = " " + score;
    }

    public void AddedPoints()
    {
    }

    public void DeletingPoints()
    {
        score -= 30;
    }


    public void CheckCurrentActiveScene()
    {

    }




    // Update is called once per frame
    void Update ()
    {
       CheckCurrentActiveScene();

    }

}