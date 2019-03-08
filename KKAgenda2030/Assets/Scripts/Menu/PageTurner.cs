using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Pages { CoverPage, Page1, Page2, Page3 };

public class PageTurner : MonoBehaviour {

    public Pages page;

    public GameObject[] pages;
    public GameObject[] pageContent;

    public Button[] buttons;
    public GameObject nextPageButton;
    public GameObject previousPageButton;

    public int pageIndex;
    public List<Image> fadeImages = new List<Image>();

    PersistentData pd;
    public GameObject pdPrefab;

    public List<Image> rightArrows;
    public List<Image> leftArrows;




    void Awake () {

        pd = FindObjectOfType<PersistentData>();
        if (pd == null) {
            var pdgo = Instantiate(pdPrefab);
            pd = pdgo.GetComponent<PersistentData>();
        }
        //page = Pages.Cover;
        //pageIndex = 0;
        pageIndex = pd.pageIndex;
        //foreach (GameObject page in pages) {
        //    if (page != )
        //    page.SetActive()
        //}
        for (int i = 0; i < pages.Length; i++) {
            if (pages[i] != pages[pageIndex])
                pages[i].SetActive(false);
            else
                pages[i].SetActive(true);
        }
    }

    private void Start() {
        fadeImages[pageIndex].GetComponent<Animator>().Play("FadeOut");
        ButtonToggle();
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

            Vector3 pos = Vector3.zero;
            //pages[pageIndex - 1].GetComponent<RectTransform>().position = pos;
            pageContent[pageIndex - 1].GetComponent<RectTransform>().localPosition = pos;

            pages[pageIndex - 1].GetComponent<Animator>().Play("PageBack");

            yield return new WaitForSeconds(0.5f);

            pages[pageIndex].SetActive(false);
            pageIndex--;
            foreach (Button b in buttons) b.interactable = true;
            if (pageIndex == 0) previousPageButton.SetActive(false);
            page = Pages.CoverPage + pageIndex;
            ButtonToggle();

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

            Vector3 pos = Vector3.zero;
            //pages[pageIndex + 1].GetComponent<RectTransform>().position = pos;
            pageContent[pageIndex + 1].GetComponent<RectTransform>().localPosition = pos;

            pages[pageIndex].GetComponent<Animator>().Play("Page");
            
            yield return new WaitForSeconds(0.5f);

            pages[pageIndex].SetActive(false);
            pageIndex++;
            foreach (Button b in buttons) b.interactable = true;
            if (pageIndex == pages.Length - 1) nextPageButton.SetActive(false);
            page = Pages.CoverPage + pageIndex;
            ButtonToggle();
        }
    }

    void ButtonToggle() {
        // Pageindexin mukaisesta gameobjectista tarvitaan image joka asetetaan target graphiciksi buttoniin
        foreach (var item in rightArrows) {
            item.gameObject.SetActive(false);
        }
        rightArrows[pageIndex].gameObject.SetActive(true);
        nextPageButton.GetComponent<Button>().targetGraphic = rightArrows[pageIndex];
        foreach (var item in leftArrows) {
            item.gameObject.SetActive(false);
        }
        leftArrows[pageIndex].gameObject.SetActive(true);
        previousPageButton.GetComponent<Button>().targetGraphic = leftArrows[pageIndex];
    }

}
