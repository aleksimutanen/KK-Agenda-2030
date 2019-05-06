using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onExitCollider : MonoBehaviour
{
    public List<GameObject> images = new List<GameObject>();
    


    private void OnTriggerExit(Collider other)
    {

        foreach (var decal in images)
        {
            if (other.name.Contains("Clone"))
            {
                print("Triggeri poistuu alueelta");


                decal.GetComponent<BoxCollider>().enabled = true;

                print("Collider takaisin päälle");

            }
        }


    }

}