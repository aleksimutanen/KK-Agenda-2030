using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldManager : MonoBehaviour
{
    public GameObject decalsFolder;   

    public GameObject[] images = new GameObject[8];


    void Update()
        {


            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            {
                if (Input.GetMouseButtonDown(0))
                {

                    print(hit.point);
                
                    var nGo = new GameObject("temp");

                    GameObject go = Instantiate(nGo, hit.point, Quaternion.identity);
                    print("Luodaan uusi image");
                    go.transform.parent = decalsFolder.transform;
                    go.gameObject.AddComponent<Image>();
                    // go.gameObject.GetComponent<Image>().sprite = hit.collider.;
                    go.gameObject.AddComponent<ItemDragHandler>();
                    go.layer = 5;
                    // hit.collider.gameObject.SetActive(false);
                   go.transform.position = (Input.mousePosition);


                }
            }
        }
}