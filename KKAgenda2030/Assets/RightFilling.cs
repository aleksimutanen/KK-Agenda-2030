using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightFilling : MonoBehaviour
{
    public BoxCollider2D fillingReqArea;

    private void OnTriggerStay2D(Collider2D other)
    {
        fillingReqArea.GetComponent<BoxCollider2D>().enabled = false;

        if (other.GetComponent<Draging>().dragging == false)
        {
            fillingReqArea.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
       
      
            fillingReqArea.GetComponent<BoxCollider2D>().enabled = true;

        
    }
}
