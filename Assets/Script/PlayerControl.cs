using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {


    [HideInInspector]
    public bool jump = false;
    [HideInInspector]
    public bool doubleJump = false;

    //user define player values
    public float speed;
    public float jumpForce;

    //user define bullet values
    public float bulletSpeed;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    //user define / future triggerable
    public bool canDoubleJump;

    private Rigidbody2D rb2d;

    private Transform groundCheck;
    private bool grounded = false;

    void Awake()
    {
        groundCheck = transform.Find("groundCheck");
    }
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }

        if (Input.GetKeyDown(KeyCode.W))
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
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal * speed, rb2d.velocity.y);
        rb2d.velocity = movement;
        /*float movejump;
        if (moveVertical <= 0)
            movejump = 0;
        else
            movejump = moveVertical * jumpForce;
        rb2d.AddForce(new Vector2(0, movejump));*/

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

        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed, 0);

        Destroy(bullet, 2.0f);
    }
}
