using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrashType { Glass, Compost, Plastic, Waste };

public class Trash : MonoBehaviour {
    public TrashType kind;
}
