using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrashCountScript : MonoBehaviour
{
    public static int totalThrashCount;
    Text thrashText;

    Spawner spawner;


    void Start()
    {
        thrashText = GetComponent<Text>();
        spawner = FindObjectOfType<Spawner>();
    }

    void Update()
    {
        totalThrashCount = spawner.rubbish.Count;
        thrashText.text = "Roskia jäljellä: " + totalThrashCount;

    }
}
