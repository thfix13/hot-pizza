using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour {

    public GameObject movementSel;
    public GameObject movementSel2;
    public GameObject attackSel;
    public GameObject attackSel2;
    public GameObject defenseSel;
    public GameObject defenseSel2;

    public GameObject movementSelP2;
    public GameObject movementSel2P2;
    public GameObject attackSelP2;
    public GameObject attackSel2P2;
    public GameObject defenseSelP2;
    public GameObject defenseSel2P2;

    public static Vector2Int P1selection;
    public static Vector2Int P2selection;
    private bool secondpress1 = false;
    private bool secondpress2 = false;
    private bool thirdpress1 = false;
    private bool thirdpress2 = false;

    public bool IsReady()
    {
        if ((P1selection.x != 0) && (P1selection.y != 0) && (P2selection.x != 0) && (P2selection.y != 0)) return true;
        else return false;
    }

    public void movementbutton1press()
    {
        if (!secondpress1)
        {
            P1selection.x = 1;
            movementSel.SetActive(true);
            secondpress1 = true;
        }
        else if (!thirdpress1)
        {
            P1selection.y = 1;
            movementSel2.SetActive(true);
            thirdpress1 = true;
        }   
    }

    public void attackbutton1press()
    {
        if (!secondpress1)
        {
            P1selection.x = 2;
            attackSel.SetActive(true);
            secondpress1 = true;
        }
        else if (!thirdpress1)
        {
            P1selection.y = 2;
            attackSel2.SetActive(true);
            thirdpress1 = true;
        }
    }

    public void defensebutton1press()
    {
        if (!secondpress1)
        {
            P1selection.x = 3;
            defenseSel.SetActive(true);
            secondpress1 = true;
        }
        else if (!thirdpress1)
        {
            P1selection.y = 3;
            defenseSel2.SetActive(true);
            thirdpress1 = true;
        }
    }

    public void movementbutton2press()
    {
        if (!secondpress2)
        {
            P2selection.x = 1;
            movementSelP2.SetActive(true);
            secondpress2 = true;
        }
        else if (!thirdpress2)
        {
            P2selection.y = 1;
            movementSel2P2.SetActive(true);
            thirdpress2 = true;
        }
    }

    public void attackbutton2press()
    {
        if (!secondpress2)
        {
            P2selection.x = 2;
            attackSelP2.SetActive(true);
            secondpress2 = true;
        }
        else if (!thirdpress2)
        {
            P2selection.y = 2;
            attackSel2P2.SetActive(true);
            thirdpress2 = true;
        }
    }

    public void defensebutton2press()
    {
        if (!secondpress2)
        {
            P2selection.x = 3;
            defenseSelP2.SetActive(true);
            secondpress2 = true;
        }
        else if (!thirdpress2)
        {
            P2selection.y = 3;
            defenseSel2P2.SetActive(true);
            thirdpress2 = true;
        }
    }
}
