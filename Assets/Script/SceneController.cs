﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
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
        SceneManager.LoadScene("HotPizzaLoadout");
    }
}