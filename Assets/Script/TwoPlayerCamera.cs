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

    public float dampTime = 0.3f;
    private Vector3 velocity = Vector3.zero;

    private Vector2 minBound;
    private Vector2 maxBound;

    void Start() {
        // find the corners of the space usable by the camera
        Collider2D col = GameObject.FindWithTag("camera bounds").GetComponent<Collider2D>();
        minBound = col.bounds.min;
        maxBound = col.bounds.max;
    }

	// Update is called once per frame
	void LateUpdate() {
        Vector2 bottomLeft;
        Vector2 topRight;

        FindCorners(out bottomLeft, out topRight);

        float ySize;        

        SetCameraSize(out ySize, bottomLeft, topRight);
        SetCameraPos(bottomLeft, topRight);
    }

    // Find the corners of the screen, inside the pre-defined bounds
    void FindCorners(out Vector2 bottomLeft, out Vector2 topRight) {
        float leftX = Mathf.Min(player1.transform.position.x, player2.transform.position.x) - 0.5f * padding;
        leftX = leftX < minBound.x? minBound.x : leftX;

        float rightX = Mathf.Max(player1.transform.position.x, player2.transform.position.x) + 0.5f * padding;
        rightX = rightX > maxBound.x? maxBound.x : rightX;

        float downY = Mathf.Min(player1.transform.position.y, player2.transform.position.y) - 0.5f * padding;
        downY = downY < minBound.y? minBound.y : downY;

        float upY = Mathf.Max(player1.transform.position.y, player2.transform.position.y) + 0.5f * padding;
        upY = upY > maxBound.y? maxBound.y : upY;

        bottomLeft = new Vector2(leftX, downY);
        topRight = new Vector2(rightX, upY);


    }

    // Set the camera position given the corners of the screen
    void SetCameraPos(Vector2 bottomLeft, Vector2 topRight)
    {
        Vector3 middle = new Vector3((bottomLeft.x + topRight.x) * 0.5f, (bottomLeft.y + topRight.y) * 0.5f);
        Vector3 point = mainCam.WorldToViewportPoint(middle);
        Vector3 delta = middle - mainCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
        Vector3 destination = mainCam.transform.position + delta;

        mainCam.transform.position = Vector3.SmoothDamp(mainCam.transform.position, destination, ref velocity, dampTime);
    }

    // Set and return the camera size, given the corners
    void SetCameraSize(out float ySize, Vector2 bottomLeft, Vector2 topRight)
    {
        float minSizeX = minSizeY * Screen.width / Screen.height;

        float width = (topRight.x - bottomLeft.x) * 0.5f;
        float height = (topRight.y - bottomLeft.y) * 0.5f;

        float camSizeX = Mathf.Max(width, minSizeX);
        ySize = Mathf.Max(height,
            camSizeX * Screen.height / Screen.width, minSizeY);
        mainCam.orthographicSize = ySize;
    }
}
