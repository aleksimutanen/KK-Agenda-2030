using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrashGameManager : MonoBehaviour {

    public static TrashGameManager instance = null;

    public List<GameObject> Trashes;
    public GameObject spawner;
    public GameObject player;

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

    // Update is called once per frame
    void Update () {
		
	}
}
