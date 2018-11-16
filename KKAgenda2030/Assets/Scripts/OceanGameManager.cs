﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OceanGameManager : MonoBehaviour {
    enum TimerType { Trash, Net, Food};
    public static OceanGameManager instance;

    public int foodEaten;
    public int trashEaten;

    public float objectDistanceThreshold;

    public int[] levelFoodAmounts;
    public int[] levelTrashAmounts;
    public GameObject foodPrefab;
    public GameObject trashPrefab;
    public Transform[] foodFolders;
    public Transform[] trashFolders;

    public int levelIndex;

    public LayerMask collectable;

    public int score;
    [SerializeField] int foodScore;
    [SerializeField] int trashPenalty;

    public Slider scoreSlider;
    float scoreTimer = -1f;
    List<float> scoreTimers = new List<float>();
    List<TimerType> timerTypes = new List<TimerType>();
    //bool hitTrash;
    //bool hitNet;
    //bool hitFood;

    void Awake() {
        if (instance)
            Debug.LogError("2+ SeaManagers found!");
        instance = this;
    }

    void Start() {
        scoreSlider.value = 0;
        SpawnObjects(levelFoodAmounts[levelIndex], foodPrefab, foodFolders[levelIndex]);
        SpawnObjects(levelTrashAmounts[levelIndex], trashPrefab, trashFolders[levelIndex]);
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
                    LoseScore(trashPenalty * 2);
                else if (timerTypes[i] == TimerType.Food)
                    GainScore(foodScore);
                i++;
            }
        }

        //if (scoreTimer > 0) {
        //    scoreTimer -= Time.deltaTime;
        //    LoseScore(trashPenalty);
        //}
    }

    void SpawnObjects(int objectAmount, GameObject objectPrefab, Transform objectFolder) {
        while (objectAmount > 0) {
            var pos = RandomizePosition();
            if (CheckPosition(pos)) {
                GameObject obj = Instantiate(objectPrefab, pos, transform.rotation);
                obj.transform.parent = objectFolder;
                objectAmount--;
            }
        }
    }

    Vector3 RandomizePosition() {
        Vector3 pos = new Vector3(Random.Range(-8.5f, 8.5f), 0.1f, Random.Range(-12f, 12f));
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
        yield return new WaitForSeconds(2f);
        scoreSlider.value = Mathf.RoundToInt(scoreSlider.value);
        print(scoreSlider.value);
        if (scoreSlider.value >= 80) {
            print("you get 3 stars");
        } else if (scoreSlider.value >= 50) {
            print("you get 2 stars");
        } else if (scoreSlider.value >= 20) {
            print("you get 1 star");
        } else {
            print("you get no stars");
        }

        if (levelIndex < 3) {
            NextLevel();
        } else {
            //foodFolders[levelIndex - 1].gameObject.SetActive(false);
            //trashFolders[levelIndex - 1].gameObject.SetActive(false);
            print("game complete");
            GameComplete();
        }
        yield return null;
    }

    void NextLevel() {
        foodFolders[levelIndex - 1].gameObject.SetActive(false);
        trashFolders[levelIndex - 1].gameObject.SetActive(false);

        SpawnObjects(levelFoodAmounts[levelIndex], foodPrefab, foodFolders[levelIndex]);
        SpawnObjects(levelTrashAmounts[levelIndex], trashPrefab, trashFolders[levelIndex]);

        FindObjectOfType<CharacterMover>().ResetCharacter();
        scoreSlider.value = 0f;
        foodEaten = 0;
    }

    void GameComplete() {

    }

    public void HitFood() {
        foodEaten++;
        //score += foodScore;
        scoreTimers.Add(1f);
        timerTypes.Add(TimerType.Food);
        CheckFoodAmount(foodEaten, levelFoodAmounts[levelIndex]);
    }

    public void HitTrash() {
        //trashEaten++;
        //score -= trashPenalty;
        //hitTrash = true;

        scoreTimers.Add(1f);
        timerTypes.Add(TimerType.Trash);

        //scoreTimer = scoreTimer < 0 ?
        //    1 :
        //    scoreTimer + 1;
    }

    public void HitNet() {
        //score -= trashPenalty * 2;

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
