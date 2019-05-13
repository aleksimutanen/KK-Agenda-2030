using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectScaler : MonoBehaviour
{

    public RectTransform rec;


   
    public void Start()
    {
        rec.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }
}
