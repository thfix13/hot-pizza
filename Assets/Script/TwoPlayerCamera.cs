using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//credit code from :
//https://answers.unity.com/questions/674225/2d-camera-to-follow-two-players.html
//https://answers.unity.com/questions/29183/2d-camera-smooth-follow.html

public class TwoPlayerCamera : MonoBehaviour {

    public GameObject player1;
    public GameObject player2;
    public Camera mainCam;

    public float minSizeY;
    public float padding;

    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;

	// Update is called once per frame
	void LateUpdate() {
        SetCameraPos();
        SetCameraSize();
    }

    void SetCameraPos()
    {
        Vector3 middle = (player1.transform.position + player2.transform.position) * 0.5f;
        Vector3 point = mainCam.WorldToViewportPoint(middle);
        Vector3 delta = middle - mainCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
        Vector3 destination = mainCam.transform.position + delta;

        mainCam.transform.position = Vector3.SmoothDamp(mainCam.transform.position, destination, ref velocity, dampTime);
    }

    void SetCameraSize()
    {
        float minSizeX = minSizeY * Screen.width / Screen.height;

        float width = Mathf.Abs(player1.transform.position.x - player2.transform.position.x) * 0.5f + padding;
        float height = Mathf.Abs(player1.transform.position.y - player2.transform.position.y) * 0.5f + padding;

        float camSizeX = Mathf.Max(width, minSizeX);
        mainCam.orthographicSize = Mathf.Max(height,
            camSizeX * Screen.height / Screen.width, minSizeY);
    }
}
