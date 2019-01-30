using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyRepair : MonoBehaviour
{

    ToyRepairManager tRM;
    public int objectsfalse = 0;
    public bool[] isRepaired;

    private void Start()
    {
      gameObject.SetActive(true);
        tRM = GameObject.FindObjectOfType<ToyRepairManager>();
    }

    private void OnCollisionEnter(Collision col)  
    {
        if (col.gameObject.tag == "RepairCube")
        {
              print("Nyt osuu!");
              
              gameObject.SetActive(false);
              objectsfalse++;
            
        }

        if (!gameObject.activeSelf)
        {

            foreach (bool ready in tRM.isRepaired)
            {
                
                print("yksi on valmis");
                tRM.isRepaired.SetValue(true, ready);
                
            }
        }

        
        
    }
}