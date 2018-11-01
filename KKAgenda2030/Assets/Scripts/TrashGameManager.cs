using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameState { Level1, Level2, Level3 };

public class TrashGameManager : MonoBehaviour {

    public static TrashGameManager instance = null;

    public Text statusText;
    public Text scoreText;
    public GameState state;
    public List<GameObject> Trashes;
    public GameObject spawner;
    public GameObject player;
    private int score = 0;

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
        spawner = GetComponent<GameObject>();
        player = GetComponent<GameObject>();

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

    // Update is called once per frame
    void Update () {
		
	}
}
