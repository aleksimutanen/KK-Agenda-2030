using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyChanged : MonoBehaviour
{
    ToyGameManager  tGM;
    toyGoals tg;
    public List<GameObject> endPoints;
    public List<GameObject> goalPoints;
    public float speed;
    // Start is called before the first frame update
    void Awake()
    {
        tGM = GetComponent<ToyGameManager>();
        tg = GetComponent<toyGoals>();
    }

    // Update is called once per frame

    void Start()
    {
        searchingOfGoalPoints();
    }

    public void searchingOfGoalPoints()
    {
        GameObject point1 = GameObject.FindWithTag("ToyP1");
        GameObject point2 = GameObject.FindWithTag("ToyP2");
        GameObject point3 = GameObject.FindWithTag("ToyP3");
        GameObject point4 = GameObject.FindWithTag("ToyP4");
        GameObject point5 = GameObject.FindWithTag("ToyP5");
        GameObject point6 = GameObject.FindWithTag("ToyP6");
        
        // Lisätään ne listaan.

        endPoints.Add(point1);
        endPoints.Add(point2);
        endPoints.Add(point3);
        endPoints.Add(point4);
        endPoints.Add(point5);
        endPoints.Add(point6);


        //for (int i = 0; i < tGM.kids.Count; i++)
        //{
        //    tg.tGoals[i].position = this.goalPoints.Add(tg.tGoals[i].transform.position);
        //}

        if (endPoints.Count > 0)

        {
           int i = 0;
            if(gameObject.name == "AirplaneFinalToy(Clone)")
            {
                goalPoints.Add(endPoints[0]);

                endPoints.RemoveAt(0);
            }
        

        
            if (gameObject.name == "FootballFinalToy(Clone)")
            {
                goalPoints.Add(endPoints[1]);
                endPoints.RemoveAt(1);

            }
        

        
            if (gameObject.name == "BearFinalToy(Clone)")
            {
                goalPoints.Add(endPoints[2]);
                endPoints.RemoveAt(2);

            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "ToyGoal")
        {
            int i = 0;
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

            print("Siirrytään kohteeseen" + goalPoints[i]);
        }
    }


}
