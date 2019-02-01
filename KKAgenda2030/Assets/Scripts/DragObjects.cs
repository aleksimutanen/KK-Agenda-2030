using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObjects : MonoBehaviour {


    Vector3 dist;
    float posX;
    float posY;
    float returnSpeed = 20;
    float hintTimer;

    public Vector3 startPos;
    public bool dragging = false;

    GSpawners gs;



    void Start() {
        startPos = transform.position;
        gs = FindObjectOfType<GSpawners>();
    }

    void Update() {

        hintTimer += Time.deltaTime;
        if (hintTimer > 10f) {
            Hint();
            hintTimer = 0f;
        }

        if (dragging) {
            Vector3 curPos = new Vector3(Input.mousePosition.x - posX, Input.mousePosition.y - posY, dist.z);
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(curPos);
            transform.position = worldPos;
        } else {
            var distance = Vector3.Distance(transform.position, startPos);
            if (distance > 0.01f) { // roska liikuu lähtöpistettä kohti
                GetComponent<Collider>().enabled = false;
                var newPos = Vector3.MoveTowards(transform.position, startPos, returnSpeed * Time.deltaTime);
                transform.position = newPos;
                if (Vector3.Distance(transform.position, startPos) <= 0.01f) { // roska on lähtöpisteessä
                    GetComponent<Collider>().enabled = true;
                }
            }
        }
    }

   public void OnDraggingEnd() {
        dragging = false;
        var mousePos = Camera.main.ScreenPointToRay(Input.mousePosition);
        TrashDestroy td = null;
        RaycastHit hit;
        if (Physics.Raycast(mousePos, out hit)) {
            td = hit.collider.GetComponent<TrashDestroy>();
            // onko collidereista joku can triggeri
            if (td) {
                var temp = GetComponent<Trash>();
                if (td.acceptTypes.Contains(temp.kind)) {
                    td.EatTrash(GetComponent<Trash>());
                    TrashGameManager.instance.AddedPoints();
                    TrashGameManager.instance.ResSpawning();
                    gameObject.SetActive(false);
                } else {
                    td.SpitTrash();
                    TrashGameManager.instance.DeletingPoints();
                }
            }
        }
    }

    public void Hint() {
        var currentTrash = FindObjectOfType<Trash>();
        var trashEnum = currentTrash.GetComponent<Trash>().kind;
        // haetaan trashEnumia vastaava roskis acceptTypesin avulla, mistä soittaa animaatiota.
        var tc = FindTrashCan(trashEnum);
        if (!tc) {
            Debug.LogError("ei löytynyt oikeaa roskista");
        }
        // soittaa parentin animaattorista hintshake
        tc.GetComponent<Animator>().Play("HintShake");
    }

    TrashDestroy FindTrashCan (TrashType tt) {
        var so = FindObjectsOfType<TrashDestroy>();
        foreach (var td in so) {
            if (td.acceptTypes.Contains(tt)) {
                return td;
            }
        }
        //foreach (var item in gs.SpawnerObjects) {
        //    var td = item.GetComponent<TrashDestroy>();
        //    if (td.acceptTypes.Contains(tt)) {
        //        return td;
        //    }
        //}
        return null;
    }

    void OnMouseDown() {
        dragging = true;
        dist = Camera.main.WorldToScreenPoint(transform.position);
        posX = Input.mousePosition.x - dist.x;
        posY = Input.mousePosition.y - dist.y;
    }

    void OnMouseUp() {
        OnDraggingEnd();
    }
}
