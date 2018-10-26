using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanGameManager : MonoBehaviour {

    public static OceanGameManager instance;

    public List<Collectable> collectables = new List<Collectable>();
    public List<Avoidable> avoidables = new List<Avoidable>();
    List<Vector3> locations = new List<Vector3>();

    public float objectDistanceThreshold;

    public int foodAmount;
    public int trashAmount;
    public GameObject foodPrefab;
    public GameObject trashPrefab;

    public Transform foodFolder;
    public Transform trashFolder;

    public LayerMask collectable;

    void Awake() {
        if (instance) {
            Debug.LogError("2+ SeaManagers found!");
        }
        instance = this;
    }

    void Start() {
        SpawnObjects(foodAmount, foodPrefab, foodFolder);
        SpawnObjects(trashAmount, trashPrefab, trashFolder);
        //while (trashAmount > 0) {
        //    var pos = RandomizePosition();
        //    if (CheckPosition(pos)) {
        //        GameObject trash = Instantiate(trashPrefab, pos, transform.rotation);
        //        trash.transform.parent = trashFolder;
        //        trashAmount--;
        //    }
        //}

        //for (int k = 0; k < trashAmount; k++) {
        //    if (k == 0) {
        //        var randomLocation = new Vector3(Random.Range(-8f, 8f), 0.1f, Random.Range(-4.5f, 15.5f));
        //        g = Instantiate(trashPrefab, randomLocation, transform.rotation);
        //        g.transform.parent = trashFolder;
        //        //p = g;
        //        checkDistance.Add(g);
        //    } else {
        //        var randomLocation = new Vector3(Random.Range(-8f, 8f), 0.1f, Random.Range(-4.5f, 15.5f));
        //        //while (Vector3.Distance(p.transform.position, randomLocation) < objectDistanceThreshold) {
        //        while (Vector3.Distance(checkDistance[0].transform.position, randomLocation) < objectDistanceThreshold) {
        //            for (int f = 0; f < checkDistance.Count; f++) {
        //                if (Vector3.Distance(checkDistance[f].transform.position, randomLocation) < objectDistanceThreshold) {
        //                    //print("too close to previous");
        //                    randomLocation = new Vector3(Random.Range(-8f, 8f), 0.1f, Random.Range(-4.5f, 15.5f));
        //                }
        //            }
        //        }
        //        g = Instantiate(trashPrefab, randomLocation, transform.rotation);
        //        g.transform.parent = trashFolder;
        //        checkDistance.Add(g);
        //        //p = g;
        //    }
        //}
    }

    void SpawnObjects(int objectAmount, GameObject objectPrefab, Transform objectFolder) {
        while (objectAmount > 0) {
            var pos = RandomizePosition();
            if (CheckPosition(pos)) {
                GameObject obj = Instantiate(objectPrefab, pos, transform.rotation);
                obj.transform.parent = objectFolder;
                objectAmount--;
            }
        }
    }

    Vector3 RandomizePosition() {
        Vector3 pos = new Vector3(Random.Range(-8f, 8f), 0.1f, Random.Range(-4.5f, 15.5f));
        return pos;
    }

    bool CheckPosition(Vector3 rndPos) {
        var checkPos = Physics.OverlapSphere(rndPos, objectDistanceThreshold, collectable);
        bool hit = checkPos.Length > 0;
        return !hit;
    }

    void Update() {

    }

    public void AddFoodToList(Collectable food) {
        if (collectables.Contains(food))
            return;
        else
            collectables.Add(food);
        if (collectables.Count == foodAmount)
            print("all food found");        
    }

    public void AddTrashToList(Avoidable trash) {
        if (avoidables.Contains(trash))
            return;
        else
            avoidables.Add(trash);
    }
}
