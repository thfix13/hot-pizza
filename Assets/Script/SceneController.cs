using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public GameObject canvas;

    private void Awake()
    {
        Cursor.visible = true;
    }

    public void NextScene()
    {   
        StartCoroutine(MyCoroutine());
    }

    IEnumerator MyCoroutine()
    {
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(1);
        Destroy(canvas);
        SceneManager.LoadScene("HotPizzaLoadout");
    }
}