using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldManager : MonoBehaviour
{
    public GameObject decalsFolder;

    public List<GameObject> images = new List<GameObject>();
    private float range = 1000f;


    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {


            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity /*, 1 << 5 LayerMask.GetMask(new string[] { "UI" })*/ ))
            {


                foreach (var decal in images)
                {
                    if (decal.name == hit.collider.name)
                    {
                        GameObject go = Instantiate(decal, hit.point, Quaternion.identity);
                        //go.gameObject.transform.localPosition = new Vector3(0.0F, 0.0f, 0.0f);
                        go.gameObject.transform.localScale = new Vector3(1.1F, 0.6f, 0.0f);
                        go.gameObject.AddComponent<Draging>();
                      //  go.gameObject.GetComponent<Draging>().OnMouseDown();
                        print("Created new decal" + " " + decal.name);
                     //   go.gameObject.GetComponent<Draging>().dragging = true;
                        go.gameObject.GetComponent<BoxCollider>().isTrigger = true;

                        //if (go.gameObject.GetComponent<Draging>().dragging == true)
                        //{
                        //    go.gameObject.GetComponent<Draging>().OnMouseDown();
                        //    print("kohde sprite pois päältä");
                        //    hit.collider.gameObject.SetActive(false);
                        //}
                        //else if (go.gameObject.GetComponent<Draging>().dragging == false)
                        //{
                        //    print("kohde sprite päälle");
                        //    hit.collider.gameObject.SetActive(true);
                        //}

                        go.transform.parent = decalsFolder.transform;
                    }

                }

                /* 
              print(hit.collider.name);

              var nGo = new GameObject("temp" + " " + hit.collider.name);


              go.gameObject.tag = "Decal";
              go.gameObject.AddComponent<SpriteRenderer>();
              go.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Front";
              go.gameObject.GetComponent<SpriteRenderer>().sprite = hit.collider.gameObject.GetComponent<Image>().sprite;
              go.gameObject.GetComponent<Transform>().transform.localScale = new Vector3(0.6F, 0.6f, 0.0f);
           // go.gameObject.GetComponent<Transform>().transform.position = hit.collider.gameObject.GetComponent<RectTransform>().transform.position;
           // go.gameObject.GetComponent<Transform>().position = hit.collider.gameObject.GetComponent<Transform>().position;
           // go.gameObject.GetComponent<Transform>().position =    hit.point.x
              go.gameObject.AddComponent<ItemDragHandler>();               
              go.gameObject.AddComponent<BoxCollider>();
              go.gameObject.GetComponent<BoxCollider>().size = new Vector3(7.0f, 8.0f, 0.0f);
              go.gameObject.AddComponent<Rigidbody>();
              go.gameObject.GetComponent<Rigidbody>().useGravity = false;
              go.layer = 5;
              go.transform.position = (Input.mousePosition);
          */

            }
        }
    }
}