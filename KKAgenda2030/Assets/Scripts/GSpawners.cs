    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class GSpawners : MonoBehaviour
    {
        public Transform[] SpawnerPoints;
        public List<GameObject> SpawnerObjects;
        public GameObject canFolder;

        public bool[] spotUsed;

        void Awake()
        {

            for (int E = 0; E < SpawnerPoints.Length; E++) {
            Spawner();
            }
        }

    public void Spawner() {
            // var rnd = Random.Range(0, SpawnerObjects.Count);
            int bools = Random.Range(0, spotUsed.Length);
            int spawnPointIndex = Random.Range(0, SpawnerPoints.Length);
            int spawnObjectsIndex = Random.Range(0, SpawnerObjects.Count);
            
             while (spotUsed[spawnPointIndex] == true)
             {
                spawnPointIndex = Random.Range(0, SpawnerPoints.Length);
               // spawnPointIndex++;

             }               

        var thrashCan = Instantiate(SpawnerObjects[spawnObjectsIndex], SpawnerPoints[spawnPointIndex].position, SpawnerPoints[spawnPointIndex].rotation);
        var currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "Nikle_devscenelvl2") {
            thrashCan.transform.localScale = new Vector3(1.75f, 1.75f, 1.75f);
            }
        if (currentScene.name == "Nikle_devscenelvl3") {
            thrashCan.transform.localScale = new Vector3(2f, 2f, 2f);
        }
        thrashCan.transform.parent = canFolder.transform;
        SpawnerObjects.RemoveAt(spawnObjectsIndex);  
        spotUsed[spawnPointIndex] = true;
        }
    }