using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Pages { Cover, Sea, Earth };

public class PageTurner : MonoBehaviour {

    public Pages page;

    public GameObject[] pages;

    public Button[] buttons;
    public GameObject nextPageButton;
    public GameObject previousPageButton;

    public int pageIndex;

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
        StartCoroutine("PreviousPageAnim");
    }

   
    IEnumerator PreviousPageAnim() {
        if (pageIndex >= 1) {
            if (pageIndex == pages.Length - 1) nextPageButton.SetActive(true);
            foreach (Button b in buttons) b.interactable = false;
            pages[pageIndex - 1].SetActive(true);
            pages[pageIndex - 1].GetComponent<Animator>().Play("PageBack");

            yield return new WaitForSeconds(0.5f);

            pages[pageIndex].SetActive(false);
            pageIndex--;
            foreach (Button b in buttons) b.interactable = true;
            if (pageIndex == 0) previousPageButton.SetActive(false);
            page = Pages.Cover + pageIndex;
        }
    }

    public void NextPage() {
        StartCoroutine("NextPageAnim");
    }

    IEnumerator NextPageAnim() {
        if (pageIndex < pages.Length) {
            if (pageIndex == 0) previousPageButton.SetActive(true);
            foreach (Button b in buttons) b.interactable = false;
            pages[pageIndex + 1].SetActive(true);
            pages[pageIndex].GetComponent<Animator>().Play("Page");

            yield return new WaitForSeconds(0.5f);

            pages[pageIndex].SetActive(false);
            pageIndex++;
            foreach (Button b in buttons) b.interactable = true;
            if (pageIndex == pages.Length - 1) nextPageButton.SetActive(false);
            page = Pages.Cover + pageIndex;
        }
    }
}
