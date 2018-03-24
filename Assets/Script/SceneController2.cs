using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController2: MonoBehaviour
{
    public void NextScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}