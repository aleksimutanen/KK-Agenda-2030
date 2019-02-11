using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyRepair : MonoBehaviour
{

    ToyRepairManager tRM;
    public GameObject goalPoint;
    public List<GameObject> endPoints;
   
    public List<GameObject> goalPoints;
    public List<GameObject> ownGoalPoints;
    public float speed;


    private void Start()
    {
        goalPoint = GameObject.FindGameObjectWithTag("GoalPoint");
       
        gameObject.SetActive(true);
        tRM = GameObject.FindObjectOfType<ToyRepairManager>();

        searchingOfGoalPoints();

    }



    public void searchingOfGoalPoints()
    {

        GameObject point1 = GameObject.FindWithTag("Top");
        GameObject point2 = GameObject.FindWithTag("Mid");
        GameObject point3 = GameObject.FindWithTag("Low");

        endPoints.Add(point1);
        endPoints.Add(point2);
        endPoints.Add(point3);


        var ep = Random.Range(0, endPoints.Count);

        if (endPoints.Count > 0 )
        {
            goalPoints.Add(endPoints[ep]);
           
            endPoints.RemoveAt(ep);
        }
                                  
        
        print("Maali pointti lisätty");
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "RepairCube")
        {
            int i = 0;
            print("Osuu " + gameObject.name);
            gameObject.GetComponent<Collider>().enabled = false;
            while (Vector3.Distance(transform.position, goalPoints[i].gameObject.transform.position) > 0.01f)
            {
                var newPos = Vector3.MoveTowards(transform.position, goalPoints[i].gameObject.transform.position, speed * Time.deltaTime);
                transform.position = newPos;
            }
            if (Vector3.Distance(transform.position, goalPoints[i].gameObject.transform.position) <= 0.01f)
            {
                gameObject.GetComponent<Collider>().enabled = true;

            }

            print("Siirtyy paikkaan " + goalPoint);
        }
    }
}
