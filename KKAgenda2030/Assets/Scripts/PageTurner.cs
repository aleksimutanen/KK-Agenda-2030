using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Pages { Cover, Sea, Earth };

public class PageTurner : MonoBehaviour {

    public Pages page;

    LayerMask pageTurners;

    [SerializeField] GameObject nextPageButton;
    [SerializeField] GameObject previousPageButton;

    public GameObject[] pages;
    GameObject currentPage;
    GameObject previousPage;

    int pageIndex;

    void Start () {
        page = Pages.Cover;
        pageIndex = 0;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.RightArrow))
            NextPage();
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            PreviousPage();
        //if (Input.GetKeyDown(KeyCode.Mouse0)) {
        //    RaycastHit hit;
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    if (Physics.Raycast(ray, out hit, Mathf.Infinity, pageTurners)) {
        //        if (hit.transform.gameObject == nextPageButton)
        //            NextPage();
        //        else if (hit.transform.gameObject == previousPageButton)
        //            PreviousPage();
        //    }
        //}
    }

    void PreviousPage() {
        if (pageIndex >= 1) {
            pages[pageIndex - 1].GetComponent<Animator>().Play("PageBack");
            pageIndex--;
            page = Pages.Cover + pageIndex;
        }
    }

    void NextPage() {
        if (pageIndex < pages.Length) {
            pages[pageIndex].GetComponent<Animator>().Play("Page");
            pageIndex++;
            page = Pages.Cover + pageIndex;
        }
    }
}
