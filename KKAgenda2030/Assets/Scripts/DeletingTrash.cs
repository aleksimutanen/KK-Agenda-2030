using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletingTrash : MonoBehaviour
{
    public List<TrashType> disqTypes;
    public GameObject obj;
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
                      
            spwn.rubbish.Insert(1, obj);
            print("roskia lisätty listaan");           
                                

        }       

    }
}