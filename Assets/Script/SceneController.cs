﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void NextScene()
    {
        SceneManager.LoadScene("HotPizzaLoadout");
    }
}