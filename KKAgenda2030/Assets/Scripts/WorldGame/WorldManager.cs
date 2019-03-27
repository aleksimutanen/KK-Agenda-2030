using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldManager : MonoBehaviour
{
    public GameObject decalsFolder;

    public GameObject[] images = new GameObject[8];
    private float range = 1000f;


    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            //    print("Näppäin checkki");

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity /*, 1 << 5 LayerMask.GetMask(new string[] { "UI" })*/ ))
            {


                print(hit.collider.name);

                var nGo = new GameObject("temp" + " " + hit.collider.name);

                GameObject go = Instantiate(nGo, hit.point, Quaternion.identity);
                print("Created new decal");
                
                go.transform.parent = decalsFolder.transform;
                go.gameObject.tag = "Decal";
                go.gameObject.GetComponent<Transform>().transform.position = hit.collider.gameObject.GetComponent<RectTransform>().transform.position;
                go.gameObject.AddComponent<SpriteRenderer>();
                go.gameObject.GetComponent<SpriteRenderer>().sprite = hit.collider.gameObject.GetComponent<Image>().sprite;
                go.gameObject.AddComponent<ItemDragHandler>();
               
                go.gameObject.AddComponent<BoxCollider>();
                go.gameObject.GetComponent<BoxCollider>().size = new Vector3(10.0f, 10.0f, 0.0f);
                go.gameObject.AddComponent<Rigidbody>();
                go.gameObject.GetComponent<Rigidbody>().useGravity = false;
                go.layer = 5;
                go.transform.position = (Input.mousePosition);


            }
        }
    }
}