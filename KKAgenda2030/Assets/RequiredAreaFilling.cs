using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequiredAreaFilling : MonoBehaviour
{
    public CircleCollider2D worldsArea;
    public BoxCollider2D mapArea;



    private void OnTriggerStay2D(Collider2D other)
    {
        other.gameObject.transform.position = worldsArea.transform.position;
        other.gameObject.transform.position = mapArea.transform.position;
        
    }
}
