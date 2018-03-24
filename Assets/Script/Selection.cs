using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour {
    public static Vector2Int P1selection;
    private bool secondpress = false;

    public void movementbuttonpress()
    {
        if(!secondpress)P1selection.x = 1;
        else P1selection.y = 1;
        secondpress = true;
    }
    public void attackbuttonpress()
    {
        if (!secondpress)P1selection.x = 2;
        else P1selection.y = 2;
        secondpress = true;
    }
    public void defensebuttonpress()
    {
        if (!secondpress)P1selection.x = 3;
         else P1selection.y = 3;
        secondpress = true;
    }
  
}
