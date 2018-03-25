using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController2: MonoBehaviour
{
    //public GameObject selectionObject;

    public void NextScene()
    {
        //DontDestroyOnLoad(selectionObject);
        SceneManager.LoadScene("GameScene");
    }
}