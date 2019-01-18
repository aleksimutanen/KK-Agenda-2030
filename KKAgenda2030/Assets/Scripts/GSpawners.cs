    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

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

    public void Spawner()
        {
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
          thrashCan.transform.parent = canFolder.transform;

          SpawnerObjects.RemoveAt(spawnObjectsIndex);
               
          spotUsed[spawnPointIndex] = true;
        }
    }