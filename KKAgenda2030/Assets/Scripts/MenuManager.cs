using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public void NextLevel()
    {
        SceneManager.LoadScene("Joni_devscene");
    }

	
}
