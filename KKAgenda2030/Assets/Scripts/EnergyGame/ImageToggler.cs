using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageToggler : MonoBehaviour
{
    public List<Image> images;
    public int selectedImage;

    public void changeImage() {
        images[selectedImage].gameObject.SetActive(false);
        selectedImage = 1 - selectedImage;
        images[selectedImage].gameObject.SetActive(true);
    }
}
