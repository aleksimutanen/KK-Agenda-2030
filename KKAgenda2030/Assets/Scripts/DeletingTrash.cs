using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletingTrash : MonoBehaviour
{
    public List<TrashType> disqTypes;

    private void OnTriggerEnter(Collider other)
    {
        
        if (disqTypes.Contains(other.GetComponent<Trash>().kind))
        {
            Destroy(other.gameObject);
            TrashGameManager.instance.DeletingPoints();
            TrashGameManager.instance.UpdatePoints();
            
        }


    }






}
