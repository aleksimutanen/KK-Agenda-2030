using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PhotoDestrroyer : MonoBehaviour
{
    public GameObject GoDestroy;

    void Update()
   {
        if(GoDestroy.activeSelf)
        {
            GoDestroy.gameObject.SetActive(false);
        }

    }
}
