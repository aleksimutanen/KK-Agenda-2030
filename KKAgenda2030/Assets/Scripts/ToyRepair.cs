using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyRepair : MonoBehaviour {

    ToyRepairManager tRM;
    public GameObject goalPoint;
    public List<GameObject> endPoints;

    public List<GameObject> goalPoints;

    public Animator animator;
    DragToys2 dt2;

    private void Start() {
        goalPoint = GameObject.FindGameObjectWithTag("GoalPoint");
        tRM = GameObject.FindObjectOfType<ToyRepairManager>();
        dt2 = GetComponent<DragToys2>();
        gameObject.SetActive(true);
        searchingOfGoalPoints();
    }



    public void searchingOfGoalPoints() {
        // Haetaan maali pisteet.
        GameObject point1 = GameObject.FindWithTag("Top");
        GameObject point2 = GameObject.FindWithTag("Mid");
        GameObject point3 = GameObject.FindWithTag("Low");
        GameObject point4 = GameObject.FindWithTag("TopBear");
        GameObject point5 = GameObject.FindWithTag("BearMid");
        GameObject point6 = GameObject.FindWithTag("LowBear");
        GameObject point7 = GameObject.FindWithTag("PlaneTop");
        GameObject point8 = GameObject.FindWithTag("PlaneMid");
        GameObject point9 = GameObject.FindWithTag("PlaneLow");

        // Lisätään ne listaan.

        endPoints.Add(point1);
        endPoints.Add(point2);
        endPoints.Add(point3);
        endPoints.Add(point4);
        endPoints.Add(point5);
        endPoints.Add(point6);
        endPoints.Add(point7);
        endPoints.Add(point8);
        endPoints.Add(point9);

        // Haetaan jokaiselle osalle oma pisteensä.


        var ep = Random.Range(0, endPoints.Count);

        if (endPoints.Count > 0) {
            if (gameObject.name == "Car_body(Clone)") {
                goalPoints.Add(endPoints[0]);
                endPoints.RemoveAt(0);
            }

            if (gameObject.name == "Car_tyreF(Clone)") {
                goalPoints.Add(endPoints[1]);
                endPoints.RemoveAt(1);
            }

            if (gameObject.name == "Car_tyreB(Clone)") {
                goalPoints.Add(endPoints[2]);
                endPoints.RemoveAt(2);
            }

            if (gameObject.name == "BearToy1(Clone)") {
                goalPoints.Add(endPoints[3]);
                endPoints.RemoveAt(3);
            }

            if (gameObject.name == "BearToy2(Clone)") {
                goalPoints.Add(endPoints[4]);
                endPoints.RemoveAt(4);
            }

            if (gameObject.name == "BearToy3(Clone)") {
                goalPoints.Add(endPoints[5]);
                endPoints.RemoveAt(5);
            }

            if (gameObject.name == "PlaneToy1(Clone)") {
                goalPoints.Add(endPoints[6]);
                endPoints.RemoveAt(6);
            }

            if (gameObject.name == "PlaneToy2(Clone)") {
                goalPoints.Add(endPoints[7]);
                endPoints.RemoveAt(7);
            }

            if (gameObject.name == "PlaneToy3(Clone)") {
                goalPoints.Add(endPoints[8]);
                endPoints.RemoveAt(8);
            }
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.gameObject.tag == "RepairCube" && !dt2.dragging) {
            transform.position = goalPoints[0].transform.position;
            gameObject.GetComponent<Collider>().enabled = false;
            animator.Play("New State");
            gameObject.GetComponentInChildren<ParticleSystem>().Play();
            tRM.toysInPlace++;
        }
    }
}
