using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelChanger : MonoBehaviour {

    TrashGameManager TGM;
    public Image fakeLoad;
    Scene activeScene;

    private void Awake() {
        TGM = FindObjectOfType<TrashGameManager>();
    }

    public IEnumerator LevelChange() {
        yield return new WaitForSeconds(1f);

        TGM.endScoreSlider.gameObject.SetActive(false);
        fakeLoad.gameObject.SetActive(true);

        yield return new WaitForSeconds(4.6f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public IEnumerator LoadToMenu() {
        yield return new WaitForSeconds(1f);
        TGM.endScoreSlider.gameObject.SetActive(false);
        yield return new WaitForSeconds(4.6f);
        SceneManager.LoadScene(0);
    }
}
