using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightFilling : MonoBehaviour
{
    public List<BoxCollider2D> fillingReqAreas = new List<BoxCollider2D>();

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<Draging>().dragging == true)
        {
            fillingReqAreas[0].GetComponent<BoxCollider2D>().enabled = false;
            fillingReqAreas[1].GetComponent<BoxCollider2D>().enabled = false;
        }

        if (other.GetComponent<Draging>().dragging == false)
        {
            fillingReqAreas[0].GetComponent<BoxCollider2D>().enabled = true;
            fillingReqAreas[1].GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {


        fillingReqAreas[0].GetComponent<BoxCollider2D>().enabled = true;
        fillingReqAreas[1].GetComponent<BoxCollider2D>().enabled = true;

    }


}
