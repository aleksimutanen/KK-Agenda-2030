using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuGameManager : MonoBehaviour
{

    public List <GameObject> availableWishToys;
    public List <Transform> wishToyPos;


    public List<GameObject> draggableToys;
    public List<Transform> dragToySPts;


    void Start()
    {
        var spawnedToys = new List<GameObject>();

        for (int i = 0; i < 3; i++) {
            var rndToy = Random.Range(0, availableWishToys.Count);
            //i = Random.Range(0, wishToyPos.Count);

            var wishToy = Instantiate(availableWishToys[rndToy], wishToyPos[i].position, Quaternion.identity);
            wishToy.transform.parent = wishToyPos[i].transform;

            var child = wishToy.transform.Find("Sprite").transform.localPosition = new Vector3(0, 0.3f, 0);
            spawnedToys.Add(wishToy);
            availableWishToys.RemoveAt(rndToy);
            
        }
        for (int i = 0; i < spawnedToys.Count; i++) {
            //spawnataan varsinainen draggable lelu, niin että ei ole wishToyn kanssa sama positio / luku.
            //var rndToy = Random.Range(0, draggableToy.Count);
            var id = spawnedToys[i].GetComponent<ToyID>().ID;
            var draggablePrefab = draggableToys.Find(x => x.GetComponent<ToyID>().ID == id);

            //var rndPos = Random.Range(0, dragToySPts.Count);
            //while (rndPos == i) {
            //    rndPos = Random.Range(0, dragToySPts.Count);
            //}

            var rndPos = 0;
            do {
                rndPos = Random.Range(0, dragToySPts.Count);
            } while (rndPos == i);

            var draggableToy = Instantiate(draggablePrefab, dragToySPts[rndPos].position, Quaternion.identity);
            draggableToy.transform.parent = dragToySPts[i].transform;
        }




    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
