/* taken from
 * https://answers.unity.com/questions/11892/how-would-you-make-an-energy-bar-loading-progress.html
 * https://forum.unity.com/threads/change-GUI-box-color.174609/
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public int roundNumber;
    public GameObject button;
    public GameObject CursorObject;
    public GameObject player1;
    public GameObject player2;
    public Vector2 pos1 = new Vector2(20, 40);
    public Vector2 pos2 = new Vector2(520, 40);
    public Vector2 size = new Vector2(300, 20);
    public GameObject gameOverPanel;
    public UnityEngine.UI.Text gameOverText;
    public float boxSize;
    public float boxGap;
    public GameObject roundPanel;
    public Text roundText;
    private float barDisplay1; //current progress
    private float barDisplay2; //current progress
    private int numLives1;
    private int numLives2;
    private Texture2D emptyTex;
    private Texture2D fullTex;

    private Texture2D MakeTex(int width, int height, Color col)
    {
        Color[] pix = new Color[width * height];
        for (int i = 0; i < pix.Length; ++i)
        {
            pix[i] = col;
        }
        Texture2D result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();
        return result;
    }

    private void Start()
    {
        barDisplay1 = 1;
        barDisplay2 = 1;
        roundNumber = 1;
        gameOverPanel.SetActive(false);
        /*emptyTex = Texture2D.whiteTexture;
        fullTex = Texture2D.blackTexture;*/
        emptyTex = MakeTex((int) (2*size.x), (int) size.y,Color.cyan);
        fullTex = MakeTex((int) (2*size.x), (int) size.y, Color.red);
        numLives1 = 3;
        numLives2 = 3;
        boxSize = 25;
        boxGap = 30;
        //Debug.Log(Selection.P1selection.x.ToString()+" "+Selection.P1selection.y.ToString()+" "+ Selection.P2selection.x.ToString()+" "+ Selection.P2selection.y.ToString());
        StartCoroutine(displayRound());
    }

    IEnumerator displayRound()
    {
        player1.GetComponent<PlayerControl>().activated = false;
        player2.GetComponent<PlayerControl>().activated = false;
        roundPanel.SetActive(true);
        roundText.text = "Round " + roundNumber.ToString();
        yield return new WaitForSeconds(2);
        roundPanel.SetActive(false);
        player1.GetComponent<PlayerControl>().activated = true;
        player2.GetComponent<PlayerControl>().activated = true;
    }

    public void OnGUI()
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

        GUI.BeginGroup(new Rect(pos1.x, pos1.y + boxGap, 3 * boxSize + 2 * boxGap, boxSize));
        GUI.Box(new Rect(0, 0, boxSize, boxSize), numLives1.ToString());
        GUI.EndGroup();

        GUI.BeginGroup(new Rect(pos2.x, pos2.y + boxGap, 3 * boxSize + 2 * boxGap, boxSize));
        GUI.Box(new Rect(0, 0, boxSize, boxSize), numLives2.ToString());
        GUI.EndGroup();
    }

    public void Update()
    {
        numLives1 = player1.GetComponent<PlayerStatus>().life;
        numLives2 = player2.GetComponent<PlayerStatus>().life;
        if (player1) barDisplay1 = player1.GetComponent<PlayerStatus>().health / player1.GetComponent<PlayerStatus>().fullhealth;
        if (player2) barDisplay2 = player2.GetComponent<PlayerStatus>().health / player2.GetComponent<PlayerStatus>().fullhealth;
        
        if (player1.GetComponent<PlayerStatus>().life <= 0 || player2.GetComponent<PlayerStatus>().life <= 0)
        {
            GameOver();
        }
        else if (barDisplay1 <= 0 || barDisplay2 <= 0)
        {
            roundNumber++;
            StartCoroutine(displayRound());
        }
    }

    public void GameOver()
    {
        player1.GetComponent<PlayerControl>().activated = false;
        player2.GetComponent<PlayerControl>().activated = false;
        gameOverPanel.SetActive(true);
        if (player1.GetComponent<PlayerStatus>().life <= 0)
        {
            gameOverText.text = "Player 2 wins! But do the Pepperoni?";

        }
        else gameOverText.text = "Player 1 wins! But do the Pineapples?";
        button.SetActive(true);
        CursorObject.GetComponent<CursorScript>().Reenable();
    }
   
}