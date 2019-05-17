using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuGameReset : MonoBehaviour {

    public List<Animal> animals;
    public List<TrashBehaivor> trashes;
    public List<GameObject> animalHalos;

    private void OnEnable() {
        foreach (var item in trashes) {
            item.GetComponent<SpriteRenderer>().enabled = true;
            item.GetComponent<Button>().enabled = true;
        }
        foreach (var item in animals) {
            item.FindTrashes();
        }

        foreach (var item in animalHalos) {
            item.SetActive(false);
        }

    }
}
