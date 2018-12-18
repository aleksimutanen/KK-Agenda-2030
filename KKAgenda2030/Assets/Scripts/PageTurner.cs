using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Pages { Cover, Sea, Earth };

public class PageTurner : MonoBehaviour {

    public Pages page;

    public GameObject[] pages;

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
    }

    public void PreviousPage() {
        if (pageIndex >= 1) {
            pages[pageIndex - 1].GetComponent<Animator>().Play("PageBack");
            pageIndex--;
            page = Pages.Cover + pageIndex;
        }
    }

    public void NextPage() {
        if (pageIndex < pages.Length) {
            pages[pageIndex].GetComponent<Animator>().Play("Page");
            pageIndex++;
            page = Pages.Cover + pageIndex;
        }
    }
}
