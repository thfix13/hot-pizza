using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneController3 : MonoBehaviour {

    // Use this for initialization
    public void NextScene()
    {
        SceneManager.LoadScene("HotPizzaTitle");
    }
}
