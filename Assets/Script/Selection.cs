using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour {

    public static Vector2Int P1selection;
    public static Vector2Int P2selection;
    private bool secondpress1 = false;
    private bool secondpress2 = false;

    public void movementbutton1press()
    {
        if(!secondpress1)P1selection.x = 1;
        else P1selection.y = 1;
        secondpress1 = true;
    }
    public void attackbutton1press()
    {
        if (!secondpress1)P1selection.x = 2;
        else P1selection.y = 2;
        secondpress1 = true;
    }
    public void defensebutton1press()
    {
        if (!secondpress1)P1selection.x = 3;
         else P1selection.y = 3;
        secondpress1 = true;
    }
    public void movementbutton2press()
    {
        if (!secondpress2) P2selection.x = 1;
        else P2selection.y = 1;
        secondpress2 = true;
    }
    public void attackbutton2press()
    {
        if (!secondpress2) P2selection.x = 2;
        else P2selection.y = 2;
        secondpress2 = true;
    }
    public void defensebutton2press()
    {
        if (!secondpress2) P2selection.x = 3;
        else P2selection.y = 3;
        secondpress2 = true;
    }

}
