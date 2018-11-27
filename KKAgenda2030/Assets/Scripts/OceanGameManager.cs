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
    public Transform[] foodFolders;
    public Transform[] trashFolders;
    public GameObject[] trashPrefabs;
    public GameObject foodPrefab;
    public GameObject nets;

    public int levelIndex;

    public LayerMask collectable;

    public int score;
    [SerializeField] int foodScore;
    [SerializeField] int trashPenalty;

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
        levelIndex = 0;
        scoreSlider.value = 0;
        roundEndSlider.gameObject.SetActive(false);
        SpawnTrash(levelTrashAmounts[levelIndex], trashPrefabs, trashFolders[levelIndex]);
        SpawnFood(levelFoodAmounts[levelIndex], foodPrefab, foodFolders[levelIndex]);
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

    void Update() {
        if (Input.GetKeyDown(KeyCode.P)) {
            FindObjectOfType<CharacterMover>().GrowScale();
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
                    LoseScore(trashPenalty);
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

        print("level transition start");
        FindObjectOfType<UIManager>().OceanGameLevelComplete();
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

        yield return new WaitForSeconds(5f);

        FindObjectOfType<UIManager>().slider.GetComponent<Animator>().Play("New State");
        roundEndSlider.gameObject.SetActive(false);
        for (int i = 0; i < starImages.Length; i++) starImages[i].gameObject.SetActive(false);

        print(scoreSlider.value);
        if (scoreSlider.value >= 80) {
            print("you get 3 stars");
        } else if (scoreSlider.value >= 55) {
            print("you get 2 stars");
        } else if (scoreSlider.value >= 30) {
            print("you get 1 star");
        } else {
            print("you get no stars");
        }

        if (levelIndex < 3) {
            NextLevel();
        } else {
            print("game complete");
            GameComplete();
        }
        //yield return null;
    }

    void NextLevel() {
        foodFolders[levelIndex - 1].gameObject.SetActive(false);
        trashFolders[levelIndex - 1].gameObject.SetActive(false);

        if (levelIndex > 0) {
            //nets[levelIndex - 1].SetActive(false);
            //nets[levelIndex].SetActive(true);
            nets.SetActive(true);
        }

        SpawnTrash(levelTrashAmounts[levelIndex], trashPrefabs, trashFolders[levelIndex]);
        SpawnFood(levelFoodAmounts[levelIndex], foodPrefab, foodFolders[levelIndex]);

        FindObjectOfType<CharacterMover>().ResetCharacter();
        scoreSlider.value = 0f;
        foodEaten = 0;
    }

    void GameComplete() {

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
