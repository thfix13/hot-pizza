using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneController3 : MonoBehaviour {

    void Start() {
        // consolidated from cursorscript
        Cursor.visible = false;
    }

    // Use this for initialization
    public void NextScene()
    {
        SceneManager.LoadScene("HotPizzaTitle");
    }
}
