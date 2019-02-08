using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyRepair : MonoBehaviour
{

    ToyRepairManager tRM;
    public Transform goalPoint;
    public float speed;


    private void Start()
    {
        gameObject.SetActive(true);
        tRM = GameObject.FindObjectOfType<ToyRepairManager>();
        
    }

    //private void OnCollisionEnter(Collision col)  
    //{
    //    if (col.gameObject.tag == "RepairCube")
    //    {
    //        tRM.toysCollected.Add(gameObject);
    //        tRM.UseToyPart(this.gameObject);                         
    //    }      

    //}

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.tag == "RepairCube")
    //    {
    //        print("Osuu");
    //        gameObject.GetComponent<Collider>().enabled = false;
    //        float step = speed * Time.deltaTime;
    //        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, goalPoint.transform.position, step);

    //        print("Siirtyy paikkaan " + goalPoint);
    //    }
    //}




    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "RepairCube")
        {
            print("Osuu " + gameObject.name);
            gameObject.GetComponent<Collider>().enabled = false;
            while (Vector3.Distance(transform.position, goalPoint.position) > 0.01f) {
                var newPos = Vector3.MoveTowards(transform.position, goalPoint.position, speed * Time.deltaTime);
                transform.position = newPos;
            }
            if (Vector3.Distance(transform.position, goalPoint.position) <= 0.01f)
            {
                gameObject.GetComponent<Collider>().enabled = true;

            }

            print("Siirtyy paikkaan " + goalPoint);
        }
    }





}