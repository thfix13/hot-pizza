using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Selection : MonoBehaviour {

    private bool secondpress1 = false;
    private bool secondpress2 = false;

    public void MovementButton1Press()
    {
        if(!this.secondpress1) PlayerPrefs.SetInt("player1Sel1",1);
        else PlayerPrefs.SetInt("player1Sel2", 1);
        PlayerPrefs.Save();
        this.secondpress1 = true;
    }
    public void AttackButton1Press()
    {
        if (!secondpress1) PlayerPrefs.SetInt("player1Sel1", 2);
        else PlayerPrefs.SetInt("player1Sel2", 2);
        PlayerPrefs.Save();
        secondpress1 = true;
    }
    public void DefenseButton1Press()
    {
        if (!secondpress1) PlayerPrefs.SetInt("player1Sel1", 3);
         else PlayerPrefs.SetInt("player1Sel2", 3);
        PlayerPrefs.Save();
        secondpress1 = true;
    }
    public void MovementButton2Press()
    {
        if (!secondpress2) PlayerPrefs.SetInt("player2Sel1", 1);
        else PlayerPrefs.SetInt("player2Sel2", 1);
        PlayerPrefs.Save();
        secondpress2 = true;
    }
    public void AttackButton2Press()
    {
        if (!secondpress2) PlayerPrefs.SetInt("player2Sel1", 2);
        else PlayerPrefs.SetInt("player2Sel2", 2);
        PlayerPrefs.Save();
        secondpress2 = true;
    }
    public void DefenseButton2Press()
    {
        if (!secondpress2) PlayerPrefs.SetInt("player2Sel1", 3);
        else PlayerPrefs.SetInt("player2Sel2", 3);
        PlayerPrefs.Save();
        secondpress2 = true;
    }
}
