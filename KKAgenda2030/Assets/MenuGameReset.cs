using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuGameReset : MonoBehaviour {

    public List<Animal> animals;
    public List<TrashBehaivor> trashes;

    private void OnEnable() {
        foreach (var item in trashes) {
            item.GetComponent<SpriteRenderer>().enabled = true;
            item.GetComponent<Button>().enabled = true;
        }
        foreach (var item in animals) {
            item.FindTrashes();
        }

    }
}
