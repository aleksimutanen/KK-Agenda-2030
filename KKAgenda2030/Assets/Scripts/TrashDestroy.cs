using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TrashDestroy: MonoBehaviour {

    public List<TrashType> acceptTypes;
    public List<GameObject> Success = new List<GameObject>();
    public int maxOfList = 5;
    public int sizeOfList;
    public bool isFull = false;


    private void OnTriggerEnter(Collider other)
    {
        if (acceptTypes.Contains(other.GetComponent<Trash>().kind))
        {

            Success.Add(other.gameObject);
            sizeOfList = Success.Count;
            other.gameObject.SetActive(false);
            //Destroy(other.gameObject);
                        
            TrashGameManager.instance.AddedPoints();
            TrashGameManager.instance.UpdatePoints();

            if (sizeOfList == maxOfList)
            {             

                isFull = true;
               // print("olen täysi");
             
            }


            if (isFull == true)
            {
                TrashGameManager.instance.AllTrashcansFull();
            }
    
        }

    }
}
