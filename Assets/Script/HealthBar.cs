/* taken from
 * https://answers.unity.com/questions/11892/how-would-you-make-an-energy-bar-loading-progress.html
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public Vector2 pos1 = new Vector2(20, 40);
    public Vector2 pos2 = new Vector2(520, 40);
    public Vector2 size = new Vector2(300, 20);

    private float barDisplay1 = 1; //current progress
    private float barDisplay2 = 1; //current progress
    private Texture2D emptyTex;
    private Texture2D fullTex;

    private void Start()
    {
        emptyTex = Texture2D.whiteTexture;
        fullTex = Texture2D.blackTexture;
    }

    void OnGUI()
    {
        GUI.BeginGroup(new Rect(pos1.x, pos1.y, size.x, size.y));
        GUI.Box(new Rect(0, 0, size.x, size.y), emptyTex);
        GUI.BeginGroup(new Rect(0, 0, size.x * barDisplay1, size.y));
        GUI.Box(new Rect(0, 0, size.x, size.y), fullTex);
        GUI.EndGroup();
        GUI.EndGroup();

        GUI.BeginGroup(new Rect(pos2.x, pos2.y, size.x, size.y));
        GUI.Box(new Rect(0, 0, size.x, size.y), emptyTex);
        GUI.BeginGroup(new Rect(0, 0, size.x * barDisplay2, size.y));
        GUI.Box(new Rect(0, 0, size.x, size.y), fullTex);
        GUI.EndGroup();
        GUI.EndGroup();
    }

    void Update()
    {
        if (player1) barDisplay1 = player1.GetComponent<PlayerStatus>().health / 100;
        if (player2) barDisplay2 = player2.GetComponent<PlayerStatus>().health / 100;
    }
}