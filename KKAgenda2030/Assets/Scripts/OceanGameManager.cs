using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OceanGameManager : MonoBehaviour {

    enum TimerType { Trash, Net, Food };

    public static OceanGameManager instance;

    public int foodEaten;
    public int trashEaten;

    public float objectDistanceThreshold;

    public int[] levelFoodAmounts;
    public int[] levelTrashAmounts;
    public GameObject[] nets;
    public Transform[] foodFolders;
    public Transform[] trashFolders;
    public GameObject[] trashPrefabs;
    public List<List<Transform>> objectFolder = new List<List<Transform>>();
    public GameObject foodPrefab;

    public int levelIndex;

    public LayerMask collectable;

    public int score;
    [SerializeField] int foodScore;
    [SerializeField] int trashPenalty;
    Vector3 startingScale;
    Vector3 characterScale;

    public Slider scoreSlider;
    public Slider roundEndSlider;
    public AnimationCurve sliderAnimCurve;
    public float[] starScore;
    public Image[] starImages;
    float scoreTimer = -1f;
    List<float> scoreTimers = new List<float>();
    List<TimerType> timerTypes = new List<TimerType>();

    void Awake() {
        if (instance)
            Debug.LogError("2+ SeaManagers found!");
        instance = this;
    }

    void Start() {
        //levelIndex = 0;
        //scoreSlider.value = 0;
        //roundEndSlider.gameObject.SetActive(false);
        //SpawnTrash(levelTrashAmounts[levelIndex], trashPrefabs, trashFolders[levelIndex]);
        //SpawnFood(levelFoodAmounts[levelIndex], foodPrefab, foodFolders[levelIndex]);
        //startingScale = FindObjectOfType<CharacterMover>().transform.localScale;
        

        //while (trashAmount > 0) {
        //    var pos = RandomizePosition();
        //    if (CheckPosition(pos)) {
        //        GameObject trash = Instantiate(trashPrefab, pos, transform.rotation);
        //        trash.transform.parent = trashFolder;
        //        trashAmount--;
        //    }
        //}

        //for (int k = 0; k < trashAmount; k++) {
        //    if (k == 0) {
        //        var randomLocation = new Vector3(Random.Range(-8f, 8f), 0.1f, Random.Range(-4.5f, 15.5f));
        //        g = Instantiate(trashPrefab, randomLocation, transform.rotation);
        //        g.transform.parent = trashFolder;
        //        //p = g;
        //        checkDistance.Add(g);
        //    } else {
        //        var randomLocation = new Vector3(Random.Range(-8f, 8f), 0.1f, Random.Range(-4.5f, 15.5f));
        //        //while (Vector3.Distance(p.transform.position, randomLocation) < objectDistanceThreshold) {
        //        while (Vector3.Distance(checkDistance[0].transform.position, randomLocation) < objectDistanceThreshold) {
        //            for (int f = 0; f < checkDistance.Count; f++) {
        //                if (Vector3.Distance(checkDistance[f].transform.position, randomLocation) < objectDistanceThreshold) {
        //                    //print("too close to previous");
        //                    randomLocation = new Vector3(Random.Range(-8f, 8f), 0.1f, Random.Range(-4.5f, 15.5f));
        //                }
        //            }
        //        }
        //        g = Instantiate(trashPrefab, randomLocation, transform.rotation);
        //        g.transform.parent = trashFolder;
        //        checkDistance.Add(g);
        //        //p = g;
        //    }
        //}
    }

    public void StartGame() {
        levelIndex = 0;
        scoreSlider.value = 0;
        roundEndSlider.gameObject.SetActive(false);
        
        var b = FindObjectOfType<CharacterMover>();
        startingScale = b.transform.localScale;
        b.transform.localScale = startingScale;
        b.ResetCharacter();
        for (int i = 0; i < trashFolders.Length; i++) {
            trashFolders[i] = new GameObject().transform;
            //trashFolders[i].transform.parent = objectFolder;
            foodFolders[i] = new GameObject().transform;
            //foodFolders[i].transform.parent = objectFolder;
        }
        SpawnTrash(levelTrashAmounts[levelIndex], trashPrefabs, trashFolders[levelIndex]);
        SpawnFood(levelFoodAmounts[levelIndex], foodPrefab, foodFolders[levelIndex]);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.P)) {
            foodEaten = levelFoodAmounts[levelIndex] - 1;
            HitFood();
            //HitTrash();
        }

        for (int i = 0; i < scoreTimers.Count;) {
            scoreTimers[i] -= Time.deltaTime;
            if (scoreTimers[i] < 0) {
                scoreTimers.RemoveAt(i);
                timerTypes.RemoveAt(i);
            } else {
                if (timerTypes[i] == TimerType.Trash)
                    LoseScore(trashPenalty);
                else if (timerTypes[i] == TimerType.Net)
                    LoseScore(trashPenalty * 2f);
                else if (timerTypes[i] == TimerType.Food)
                    GainScore(foodScore);
                i++;
            }
        }
    }

    void SpawnFood(int objectAmount, GameObject objectPrefab, Transform objectFolder) {
        while (objectAmount > 0) {
            var pos = RandomizePosition();
            if (CheckPosition(pos)) {
                GameObject obj = Instantiate(objectPrefab, pos, transform.rotation);
                obj.transform.parent = objectFolder;
                objectAmount--;
                print("food spawned");
            }
        }
    }

    void SpawnTrash(int objectAmount, GameObject[] objectList, Transform objectFolder) {
        while (objectAmount > 0) {
            var pos = RandomizePosition();
            if (CheckPosition(pos)) {
                int rndIndex = Random.Range(0, objectList.Length);
                GameObject obj = Instantiate(objectList[rndIndex], pos, transform.rotation);
                obj.transform.parent = objectFolder;
                objectAmount--;
            }
        }
    }

    Vector3 RandomizePosition() {
        Vector3 pos = new Vector3(Random.Range(-8.5f, 8.5f), 0.3f, Random.Range(-12f, 12f));
        return pos;
    }

    bool CheckPosition(Vector3 rndPos) {
        var checkPos = Physics.OverlapSphere(rndPos, objectDistanceThreshold, collectable);
        bool hit = checkPos.Length > 0;
        return !hit;
    }

    void CheckFoodAmount(int foodEaten, int foodAmount) {
        if (foodEaten == foodAmount) {
            print("all food found");
            levelIndex++;
            StartCoroutine(LevelComplete());
        }
    }

    IEnumerator LevelComplete() {

        var ui = FindObjectOfType<UIManager>();
        print("level transition start");
        ui.OceanGameLevelComplete();
        FindObjectOfType<CharacterMover>().canMove = false;

        yield return new WaitForSeconds(2.5f);

        scoreSlider.value = Mathf.RoundToInt(scoreSlider.value);

        roundEndSlider.gameObject.SetActive(true);
        //roundEndSlider.value = -50f;
        float s = scoreSlider.value + 50f;
        float fillTime = 3f;

        float t = 0f;
        float fillSpeed = 1 / fillTime;

        while (t <= 1) {
            t += fillSpeed * Time.deltaTime;
            var curvedT = sliderAnimCurve.Evaluate(t);
            roundEndSlider.value = -50f + (curvedT * s);
            print(roundEndSlider.value);
            for (int i = 0; i < starImages.Length; i++) {
                if (roundEndSlider.value >= starScore[i]) {
                    starImages[i].gameObject.SetActive(true);
                    FindObjectOfType<UIManager>().LevelEndStars(starImages[i]);
                    print("star achieved");
                }
            }
            yield return null;
        }

        yield return new WaitForSeconds(3f);

        //start transition
        ui.transitionBackGround.GetComponent<Animator>().Play("OceanGameTransition");

        yield return new WaitForSeconds(1f);

        //start loading roll
        ui.slider.GetComponent<Animator>().Play("New State");
        ui.transitionCircle.gameObject.SetActive(true);
        ui.transitionCircle.GetComponent<Animator>().Play("TransitionCircle2");

        yield return new WaitForSeconds(3f);

        ui.transitionCircle.gameObject.SetActive(false);
        roundEndSlider.gameObject.SetActive(false);
        for (int i = 0; i < starImages.Length; i++) starImages[i].gameObject.SetActive(false);

        if (levelIndex < 3) {
            NextLevel();
        } else {
            print("game complete");
            GameComplete();
        }

        yield return new WaitForSeconds(1f);
        ui.transitionBackGround.GetComponent<Animator>().Play("New State");
    }

    public void ReloadLevel() {
       
        foodFolders[levelIndex].gameObject.SetActive(false);
        trashFolders[levelIndex].gameObject.SetActive(false);

        trashFolders[levelIndex] = new GameObject().transform;
        //trashFolders[levelIndex].transform.parent = objectFolder;

        foodFolders[levelIndex] = new GameObject().transform;
        //foodFolders[levelIndex].transform.parent = objectFolder;

        SpawnTrash(levelTrashAmounts[levelIndex], trashPrefabs, trashFolders[levelIndex]);
        SpawnFood(levelFoodAmounts[levelIndex], foodPrefab, foodFolders[levelIndex]);

        var b = FindObjectOfType<CharacterMover>();
        b.ResetCharacter();
        b.transform.localScale = characterScale;
        foodEaten = 0;
        scoreSlider.value = 0f;
        print("reload");
    }

    void NextLevel() {
        foodFolders[levelIndex - 1].gameObject.SetActive(false);
        trashFolders[levelIndex - 1].gameObject.SetActive(false);
        nets[levelIndex - 1].SetActive(false);

        nets[levelIndex].SetActive(true);
        SpawnTrash(levelTrashAmounts[levelIndex], trashPrefabs, trashFolders[levelIndex]);
        SpawnFood(levelFoodAmounts[levelIndex], foodPrefab, foodFolders[levelIndex]);

        var b = FindObjectOfType<CharacterMover>();
        b.ResetCharacter();
        characterScale = b.transform.localScale;
        scoreSlider.value = 0f;
        foodEaten = 0;
    }

    void GameComplete() {

    }

    public void QuitToMenu() {
        //objectFolder.GetComponentInChildren<Transform>().gameObject.SetActive(false);
        //objectFolder.gameObject.SetActive(true);
    }

    public void HitFood() {
        scoreSlider.value = Mathf.RoundToInt(scoreSlider.value);
        foodEaten++;
        scoreTimers.Add(1f);
        timerTypes.Add(TimerType.Food);
        CheckFoodAmount(foodEaten, levelFoodAmounts[levelIndex]);
    }

    public void HitTrash() {
        scoreSlider.value = Mathf.RoundToInt(scoreSlider.value);
        scoreTimers.Add(1f);
        timerTypes.Add(TimerType.Trash);
    }

    public void HitNet() {
        scoreSlider.value = Mathf.RoundToInt(scoreSlider.value);
        scoreTimers.Add(1f);
        timerTypes.Add(TimerType.Net);
    }

    void LoseScore(float scoreLost) {
        scoreSlider.value -= scoreLost * Time.deltaTime;
    }

    void GainScore(float scoreGained) {
        scoreSlider.value += scoreGained * Time.deltaTime;
    }
}
