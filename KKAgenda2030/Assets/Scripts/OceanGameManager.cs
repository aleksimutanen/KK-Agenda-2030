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
    public GameObject[] backGroundImages;
    public GameObject[] nets;
    public List<Transform> foodFolders = new List<Transform>();
    public List<Transform> trashFolders = new List<Transform>();
    public GameObject[] trashPrefabs;
    public Transform objectFolder;
    public GameObject foodPrefab;

    public int levelIndex;

    public LayerMask collectable;

    public int score;
    [SerializeField] int foodScore;
    [SerializeField] int trashPenalty;
    public Vector3 startingScale;
    Vector3 characterScale;

    public Slider scoreSlider;
    public Slider roundEndSlider;
    public Slider gameEndSlider;
    public AnimationCurve sliderAnimCurve;
    public float starsCollected;
    public float[] starScore;
    public float[] endStarScore;
    public Image[] starImages;
    public Image[] totalStars;
    public Image gameEndShark;
    public Image happySharkFace;
    public Image unHappySharkFace;

    float scoreTimer = -1f;
    List<float> scoreTimers = new List<float>();
    List<TimerType> timerTypes = new List<TimerType>();

    public string levelClear;
    public string stopMusic;
    public string sharkMusic;
    public string oneStar;

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
        scoreSlider.gameObject.SetActive(true);

        nets[levelIndex].SetActive(true);
        backGroundImages[levelIndex].SetActive(true);
        FindObjectOfType<PhoneVibrate>().nets.Clear();
        var b = FindObjectOfType<CharacterMover>();
        b.transform.localScale = startingScale;
        characterScale = b.transform.localScale;
        b.ResetCharacter();
        for (int i = 0; i < trashFolders.Count; i++) {
            trashFolders[i] = new GameObject().transform;
            trashFolders[i].transform.parent = objectFolder;
            foodFolders[i] = new GameObject().transform;
            foodFolders[i].transform.parent = objectFolder;
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
        if (Input.GetKeyDown(KeyCode.Y)) {
            foodEaten = levelFoodAmounts[levelIndex] - 1;
            levelIndex = 2;
            HitFood();
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

        Fabric.EventManager.Instance.PostEvent("stopMusic");
        Fabric.EventManager.Instance.PostEvent("levelClear");

        print("level transition start");

        var ui = FindObjectOfType<UIManager>();
        ui.OceanGameLevelComplete();
        FindObjectOfType<PhoneVibrate>().nets.Clear();
        FindObjectOfType<CharacterMover>().canMove = false;

        yield return new WaitForSeconds(2.5f);

        scoreSlider.value = Mathf.RoundToInt(scoreSlider.value);

        roundEndSlider.gameObject.SetActive(true);
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
                    ui.LevelEndStars(starImages[i]);
                    print("star achieved");
                    Fabric.EventManager.Instance.PostEvent("oneStar");
                }
            }
            yield return null;
        }

        if (roundEndSlider.value >= 79)
            starsCollected += 3f;
        else if (roundEndSlider.value >= 54)
            starsCollected += 2f;
        else if (roundEndSlider.value >= 29)
            starsCollected += 1f;

        yield return new WaitForSeconds(1f);

        if (levelIndex < 3) {

            yield return new WaitForSeconds(3f);

            //start transition
            ui.transitionBackGround.GetComponent<Animator>().Play("OceanGameTransition");
            Fabric.EventManager.Instance.PostEvent("sharkMusic");

            yield return new WaitForSeconds(1f);

            //start loading roll
            ui.slider.GetComponent<Animator>().Play("New State");
            ui.transitionCircle.gameObject.SetActive(true);
            ui.transitionCircle.GetComponent<Animator>().Play("TransitionCircle2");

            yield return new WaitForSeconds(3f);

            ui.transitionCircle.gameObject.SetActive(false);
            roundEndSlider.gameObject.SetActive(false);
            for (int i = 0; i < starImages.Length; i++) starImages[i].gameObject.SetActive(false);

            NextLevel();

        } else {
            ui.transitionBackGround.GetComponent<Animator>().Play("OceanGameGameEnd");

            yield return new WaitForSeconds(1f);

            gameEndSlider.GetComponent<Animator>().Play("OceanGameCompleted");

            yield return new WaitForSeconds(2f);

            ui.slider.GetComponent<Animator>().Play("New State");

            gameEndShark.GetComponent<Animator>().Play("UISharkSwim");
            if (starsCollected >= 7)
                happySharkFace.gameObject.SetActive(true);
            else if (starsCollected <= 3)
                unHappySharkFace.gameObject.SetActive(true);

            //s = countedStars;
            s = starsCollected;

            fillTime = 3f;
            t = 0f;
            fillSpeed = 1 / fillTime;

            while (t <= 1) {
                t += fillSpeed * Time.deltaTime;
                var curvedT = sliderAnimCurve.Evaluate(t);
                gameEndSlider.value = (curvedT * s);
                print(gameEndSlider.value);
                for (int i = 0; i < totalStars.Length; i++) {
                    if (gameEndSlider.value >= endStarScore[i]) {
                        totalStars[i].gameObject.SetActive(true);
                        ui.LevelEndStars(totalStars[i]);
                        print("star achieved");
                        Fabric.EventManager.Instance.PostEvent("oneStar");
                    }
                }
                yield return null;
            }

            yield return new WaitForSeconds(3f);

            print("slider zero");
            gameEndSlider.value = 0f;
            for (int i = 0; i < totalStars.Length; i++) totalStars[i].gameObject.SetActive(false);

            gameEndSlider.GetComponent<Animator>().Play("New State");
            happySharkFace.gameObject.SetActive(false);
            unHappySharkFace.gameObject.SetActive(false);

            QuitToMenu();
            GrandManager.instance.BackToMainMenu();

            starsCollected = 0f;
        }

        yield return new WaitForSeconds(1f);
        ui.transitionBackGround.GetComponent<Animator>().Play("New State");
    }

    public void ReloadLevel() {

        foodFolders[levelIndex].gameObject.SetActive(false);
        trashFolders[levelIndex].gameObject.SetActive(false);

        Destroy(trashFolders[levelIndex].gameObject);
        trashFolders[levelIndex] = new GameObject().transform;
        trashFolders[levelIndex].transform.parent = objectFolder;

        Destroy(foodFolders[levelIndex].gameObject);
        foodFolders[levelIndex] = new GameObject().transform;
        foodFolders[levelIndex].transform.parent = objectFolder;

        SpawnTrash(levelTrashAmounts[levelIndex], trashPrefabs, trashFolders[levelIndex]);
        SpawnFood(levelFoodAmounts[levelIndex], foodPrefab, foodFolders[levelIndex]);

        FindObjectOfType<PhoneVibrate>().nets.Clear();
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
        backGroundImages[levelIndex - 1].SetActive(false);

        backGroundImages[levelIndex].SetActive(true);
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
        FindObjectOfType<UIManager>().BackToMainMenu();
    }

    public void QuitToMenu() {
        FindObjectOfType<PhoneVibrate>().nets.Clear();
        scoreSlider.gameObject.SetActive(false);
        for (int i = 0; i < foodFolders.Count; i++) {
            Destroy(trashFolders[i].gameObject);
            Destroy(foodFolders[i].gameObject);
            nets[i].SetActive(false);
        }
        foodEaten = 0;
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
