using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {


    [HideInInspector]
    public bool jump = false;
    [HideInInspector]
    public bool doubleJump = false;
    [HideInInspector]
    public bool leftClick = false;
    [HideInInspector]
    public bool rightClick = false;

    //button define for this player
    public KeyCode jumpButton;
    public KeyCode leftButton;
    public KeyCode rightButton;
    public KeyCode shootButton;

    //user define player values
    public Rigidbody2D rb2d;
    public float speed;
    public float jumpForce;
    public bool faceLeft;

    //user define bullet values
    public float bulletSpeed;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    //user define / future triggerable
    public bool canDoubleJump;

    private bool grounded = false;

    void Start()
    {
        //rb2d = GetComponent<Rigidbody2D>();
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        if (Input.GetKeyDown(shootButton))
        {
            Fire();
        }

        if (Input.GetKeyDown(jumpButton))
        {
            if (grounded)
            {
                jump = true;
            }
            else if (canDoubleJump && doubleJump)
            {
                jump = true;
                doubleJump = false;
            }
        }

        if (Input.GetKeyDown(leftButton)) leftClick = true;
        if (Input.GetKeyUp(leftButton)) leftClick = false;
        if (Input.GetKeyDown(rightButton)) rightClick = true;
        if (Input.GetKeyUp(rightButton)) rightClick = false;
    }

    void FixedUpdate()
    {
        float moveHorizontal = 0;
        if (leftClick) moveHorizontal--;
        if (rightClick) moveHorizontal++;

        Vector2 movement = new Vector2(moveHorizontal * speed, rb2d.velocity.y);
        rb2d.velocity = movement;

        if (jump)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));

            jump = false;
            grounded = false;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("platform"))
        {
            grounded = true;
            doubleJump = true;
        }
    }

    void OnCollisionExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("platform"))
        {
            grounded = false;
        }
    }

    void Fire()
    {
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);

        float bulletVelocity;
        if (faceLeft)
            bulletVelocity = -bulletSpeed;
        else
            bulletVelocity = bulletSpeed;

        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletVelocity, 0);

        Destroy(bullet, 2.0f);
    }
}
