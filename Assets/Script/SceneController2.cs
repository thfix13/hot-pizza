using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController2: MonoBehaviour
{
    public GameObject selectionObject;

    public void NextScene()
    {
        DontDestroyOnLoad(selectionObject);
        StartCoroutine(MyCoroutine());
        
    }

    IEnumerator MyCoroutine()
    {
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("GameScene");
    }
}