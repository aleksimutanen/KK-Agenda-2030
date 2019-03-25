using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelChanger : MonoBehaviour {

    TrashGameManager TGM;
    public Image fakeLoad;
    Scene activeScene;
    public GameObject trashCanFolder;
    //public Animator endFadeAnimator;

    private void Awake() {
        TGM = FindObjectOfType<TrashGameManager>();
    }

    public IEnumerator LevelChange() {
        yield return new WaitForSeconds(1f);

        TGM.endScoreSlider.gameObject.SetActive(false);
        fakeLoad.gameObject.SetActive(true);
        fakeLoad.GetComponent<Animator>().Play("FakeLoading");
        yield return new WaitForSeconds(2.3f);
        trashCanFolder.SetActive(false);
        yield return new WaitForSeconds(2.3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public IEnumerator LoadToMenu() {
        yield return new WaitForSeconds(1f);
        TGM.endScoreSlider.gameObject.SetActive(false);
        //endFadeAnimator.Play("FadeOut");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(0);
    }
}
