using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldManager : MonoBehaviour
{
    [Header("Folder for created objects")]
    public GameObject decalsFolder;
    [Header("Images for the Game")]
    public Image targetImage;
    public Image selectionBackimage;
    public List<Image> images = new List<Image>();
    public List<Sprite> BackgroundImages = new List<Sprite>();


    [Header("Other objects")]
    public List<Button> worldSelectionButtons = new List<Button>();
    public List<GameObject> imageHolders = new List<GameObject>();
    public GameObject imageDestroyer;


    private float range = 1000f;
    private Draging createdObject;

    private void Start()
    {
        foreach (var pressed in worldSelectionButtons)
        {
            pressed.GetComponent<Image>().enabled = true;
          
        }

      
    }


    public void Update()
    {


        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            ImagesRaycasting();


            /*  Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
              RaycastHit hit;
              if (Physics.Raycast(ray, out hit, Mathf.Infinity  /*, 1 << 5 LayerMask.GetMask(new string[] { "UI" }) ))
              {
                  if (createdObject == null)
                  {
                      var foo = hit.collider.GetComponent<Draging>();

                      if (foo != null)
                      {
                          createdObject = foo;
                      }
                  }


                  foreach (var decal in stickers)
                  {
                      if (decal.name == hit.collider.name)
                      {
                          var go = Instantiate(decal, hit.point, Quaternion.identity);
                          print("Created new decal" + " " + decal.name);

                          // Skaalaus kuntoon per objekti...
                          go.gameObject.transform.localScale = new Vector3(3.0F, 3.0f, 0.0f);

                          if (decal.name == "Puu")
                          {
                              go.gameObject.transform.localScale = new Vector3(2.0F, 1.0f, 0.0f);
                          }

                          if (decal.name == "Ahven")
                          {
                              go.gameObject.transform.localScale = new Vector3(5.0F, 5.0f, 0.0f);
                          }
                          go.gameObject.AddComponent<Draging>();
                          createdObject = go.GetComponent<Draging>();
                          Destroy(createdObject.GetComponent<ColliderRadar>());
                          createdObject.MouseDown();
                          //  go.gameObject.GetComponent<Draging>().OnMouseDrag();

                          createdObject.SpriteRenderer.sortingLayerName = "Front";
                          //go.gameObject.GetComponent<Draging>().OnMouseDrag();

                          //   go.gameObject.GetComponent<Draging>().dragging = true;
                          createdObject.BoxCollider.isTrigger = true;

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

                          createdObject.transform.parent = decalsFolder.transform;
                      }

                  }


                  
                print(hit.collider.name);

                var nGo = new GameObject("temp" + " " + hit.collider.name);


                nGo.gameObject.tag = "Decal";
                nGo.gameObject.AddComponent<SpriteRenderer>();
                nGo.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Front";
                nGo.gameObject.GetComponent<SpriteRenderer>().sprite = hit.collider.gameObject.GetComponent<Image>().sprite;
                nGo.gameObject.GetComponent<Transform>().transform.localScale = new Vector3(0.6F, 0.6f, 0.0f);
                nGo.gameObject.GetComponent<Transform>().transform.position = hit.collider.gameObject.GetComponent<RectTransform>().transform.position;
                nGo.gameObject.GetComponent<Transform>().position = hit.collider.gameObject.GetComponent<Transform>().position;
                nGo.gameObject.GetComponent<Transform>().position =    hit.point.x
                nGo.gameObject.AddComponent<ItemDragHandler>();               
                nGo.gameObject.AddComponent<BoxCollider>();
                nGo.gameObject.GetComponent<BoxCollider>().size = new Vector3(7.0f, 8.0f, 0.0f);
                nGo.gameObject.AddComponent<Rigidbody>();
                nGo.gameObject.GetComponent<Rigidbody>().useGravity = false;
                nGo.layer = 5;
                nGo.transform.position = (Input.mousePosition);


              }*/
        }

        if (Input.GetMouseButton(0))
           {
               if (createdObject != null)
               {
                   createdObject.MouseDrag();
               }
           }

           if (Input.GetMouseButtonUp(0))
           {
               if (createdObject != null)
               {
                   print(createdObject);
                   createdObject.MouseUp();
                   createdObject = null;
               }
           }
    }

    private void ImagesRaycasting()
    {


        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        foreach (var im in images)
        {
            if (hit.collider == null)
            {
                continue;
            }

            if (createdObject == null)
            {
                var foo = hit.collider.GetComponent<Draging>();

                if (foo != null)
                {
                    createdObject = foo;
                }
            }

            if (im.name == hit.collider.name)
            {
                var go = Instantiate(im, hit.point, Quaternion.identity);
                print("Created new decal" + " " + hit.collider.name);
                
                go.gameObject.AddComponent<Draging>();
                
                createdObject = go.GetComponent<Draging>();
                createdObject.GetComponent<BoxCollider2D>().isTrigger = true;
                createdObject.GetComponent<BoxCollider2D>().size = new Vector2(45.0f, 45.0f);
                createdObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 45.0f);
                createdObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 45.0f);
                createdObject.GetComponent<RectTransform>().transform.localScale = new Vector3(1.0f, 1.0f,1.0f);
               // var rt = createdObject.GetComponent<RectTransform>();
               // rt.localScale = new Vector3(1.0f, 1.0f, 1.0f);



                createdObject.transform.SetParent(decalsFolder.transform);
            }
        }


    }

    public void OnBackgroudnIMage(Image ima)
    {
        targetImage.sprite = ima.sprite;

        foreach (var pressed in worldSelectionButtons)
        {
            pressed.GetComponent<Image>().enabled = false;
        }

        foreach (var mage in imageHolders)
        {
            mage.SetActive(true);
        }

        imageDestroyer.SetActive(true);
    }



}