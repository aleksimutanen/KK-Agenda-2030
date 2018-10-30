using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TrashDestroy: MonoBehaviour {
    public List<TrashType> acceptTypes;          

    private void OnTriggerEnter(Collider other)
    {      
        if (acceptTypes.Contains(other.GetComponent<Trash>().kind))
        {   
            Destroy(other.gameObject);

            TrashGameManager.instance.AddedPoints();
            TrashGameManager.instance.UpdatePoints();


            //print(other.gameObject.tag);

        }        

    }
}
