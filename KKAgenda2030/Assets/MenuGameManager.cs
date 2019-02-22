using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MenuGameManager : MonoBehaviour {

    public List<GameObject> availableWishToys;
    public List<Transform> wishToyPos;


    public List<GameObject> draggableToys;
    public List<Transform> dragToySPts;
    public List<GameObject> fillerToys;
    public List<GameObject> wishToys;
    public List<GameObject> speechBubbles;

    public List<Animator> animators;
    public List<bool> animationPlayed;
    string animationName;


    public ParticleSystem victoryParticles;

    void Start() {
        //var wishToys = new List<GameObject>();
        // WishToy arvonta
        for (int i = 0; i < 3; i++) {
            var rndToy = Random.Range(0, availableWishToys.Count);
            //i = Random.Range(0, wishToyPos.Count);

            var wishToy = Instantiate(availableWishToys[rndToy], wishToyPos[i].position, Quaternion.identity);
            var bc = wishToy.GetComponent<BoxCollider>();
            bc.enabled = !bc.enabled;
            wishToy.transform.parent = wishToyPos[i].transform;

            var child = wishToy.transform.Find("Sprite").transform.localPosition = new Vector3(0, 0.3f, 0);
            wishToys.Add(wishToy);
            availableWishToys.RemoveAt(rndToy);

        }

        var spawnToys = new List<GameObject>(wishToys);
        spawnToys.InsertRange(3, fillerToys);
        do {
            Shuffle(spawnToys);
        } while (!GoodOrder(spawnToys, wishToys));

        // spawn
        for (int i = 0; i < spawnToys.Count; i++) {
            var id = spawnToys[i].GetComponent<ToyID>().ID;
            var draggablePrefab = draggableToys.Find(x => x.GetComponent<ToyID>().ID == id);
            var draggableToy = Instantiate(draggablePrefab, dragToySPts[i].position, Quaternion.identity);
            draggableToy.transform.parent = dragToySPts[i].transform;
        }
       
        // DragToy arvonta
        //for (int i = 0; i < wishToys.Count; i++) {
        //    //spawnataan varsinainen draggable lelu, niin että ei ole wishToyn kanssa sama positio / luku.
        //    //var rndToy = Random.Range(0, draggableToy.Count);
        //    var id = wishToys[i].GetComponent<ToyID>().ID;
        //    var draggablePrefab = draggableToys.Find(x => x.GetComponent<ToyID>().ID == id);

        //    //var rndPos = Random.Range(0, dragToySPts.Count);
        //    //while (rndPos == i) {
        //    //    rndPos = Random.Range(0, dragToySPts.Count);
        //    //}

        //    var rndPos = 0;
        //    do {
        //        rndPos = Random.Range(0, dragToySPts.Count);
        //    } while (rndPos == i);

        //    var draggableToy = Instantiate(draggablePrefab, dragToySPts[rndPos].position, Quaternion.identity);
        //    draggableToy.transform.parent = dragToySPts[i].transform;
        //}
    }

    // Fisher-Yates algo
    void Shuffle(List<GameObject> l) {
        for (int j = 0; j < l.Count; j++) {
            GameObject temp = l[j];
            int randomIndex = Random.Range(j, l.Count);
            l[j] = l[randomIndex];
            l[randomIndex] = temp;
        }
    }

    bool GoodOrder(List<GameObject> spawnToys, List<GameObject> wishToys) {
        return
            spawnToys[0] != wishToys[0] && spawnToys[1] != wishToys[0] &&
            spawnToys[2] != wishToys[1] && spawnToys[3] != wishToys[1] &&
            spawnToys[4] != wishToys[2] && spawnToys[5] != wishToys[2];
    }

    public void WishToyCheck() {
        for (int i = 0; i < 3; i++) {
            var toy1 = wishToys[i].GetComponent<ToyID>().ID;
            bool toy1found = FindToyByID(i * 2, toy1) || FindToyByID(i * 2 + 1, toy1);

            if (toy1found && !animationPlayed[i]) {
                animationPlayed[i] = true;
                animationName = animators[i].gameObject.GetComponent<ClickAnimals>().animationName;
                animators[i].Play(animationName);
                
            }
        }
    }

    bool FindToyByID(int pointIndx, int ID) {
        var colliders = Physics.OverlapSphere(dragToySPts[pointIndx].position, 1f);
        foreach (var c in colliders) {
            var id = c.GetComponent<ToyID>();
            if (id && id.ID == ID) {
                c.GetComponent<BoxCollider>().enabled = !enabled;
                c.GetComponentInChildren<ParticleSystem>().Play();
                c.transform.Find("Halo").gameObject.SetActive(true);
                // play sound here?
                return true;
            }
        }
        return false;
    }

    private void Update() {
        if (!animationPlayed.Contains(false)) {
            // DO SUMTHING

            for (int i = 0; i < speechBubbles.Count; i++) {
                speechBubbles[i].SetActive(false);
            }

            for (int j = 0; j < animators.Count; j++) {
                animationName = animators[j].gameObject.GetComponent<ClickAnimals>().animationName;
                animators[j].Play(animationName);
                //maybe longer animations for final "dance"?

            }

            victoryParticles.Play();

            // Reset booleans
            for (int i = 0; i < animationPlayed.Count; i++) {
                animationPlayed[i] = false;
            }

            // another option for reset
            //animationPlayed.ConvertAll(x => false);
        }
    }
}
