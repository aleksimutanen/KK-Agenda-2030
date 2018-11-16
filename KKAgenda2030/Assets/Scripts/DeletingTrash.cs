﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletingTrash : MonoBehaviour
{
    public List<TrashType> disqTypes;
    public GameObject obj;
    public List <GameObject> gameObjects;
    Spawner spwn;


    private void Start()
    {
        spwn = FindObjectOfType<Spawner>();
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (disqTypes.Contains(other.GetComponent<Trash>().kind))
        {
            //Destroy(other.gameObject);
            TrashGameManager.instance.DeletingPoints();
            TrashGameManager.instance.UpdatePoints();

            other.gameObject.SetActive(false);
            print("roskat lajiteltu VÄÄRIN!");
            gameObjects.Add(other.gameObject);
            TrashGameManager.instance.ResSpawning();
            print("Spawnataan uusi roska");

            
            spwn.rubbish.Insert(0,obj );
           // print("roskia lisätty listaan");
                    

        }       

    }
}