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
    [HideInInspector]
    public bool activated = true;

    //button define for this player
    public KeyCode jumpButton;
    public KeyCode leftButton;
    public KeyCode rightButton;
    public KeyCode downButton;
    public KeyCode shootButton;

    //user define player values
    public Rigidbody2D rb2d;
    private PlayerStatus status;
    public bool faceLeft;

    //user define bullet values
    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    private bool grounded = false;

    //public bool IsPlayerOne;
    private bool falling = false;
    private float fallTime = 0.0f;
    private const float MIN_FALL_TIME = 0.3f;

    void Start()
    {
        //rb2d = GetComponent<Rigidbody2D>();
        //freeze the rotation of the object
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        //getting the playerstatus 
        status = GetComponent<PlayerStatus>();
    }

    void Update()
    {
        if (activated)
        {
            if (Input.GetKeyDown(shootButton))
            {
                Fire();
            }

            //checking if player can jump
            if (Input.GetKeyDown(jumpButton))
            {
                if (grounded)
                {
                    jump = true;
                }
                else if (status.canDoubleJump && doubleJump)
                {
                    jump = true;
                    doubleJump = false;
                }
            }

            if (Input.GetKeyDown(leftButton)) leftClick = true;
            if (Input.GetKeyUp(leftButton)) leftClick = false;
            if (Input.GetKeyDown(rightButton)) rightClick = true;
            if (Input.GetKeyUp(rightButton)) rightClick = false;

            if (leftClick)
            {
                faceLeft = true;
                rb2d.transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            else if (rightClick)
            {
                faceLeft = false;
                rb2d.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }

            // drop from platform
            if (Input.GetKeyDown(downButton)) {
                falling = true;
                gameObject.layer = 9;
                //rb2d.simulated = false;
                //rb2d.simulated = true;
                fallTime += Time.deltaTime;
            } else if (falling && fallTime < MIN_FALL_TIME) {
                fallTime += Time.deltaTime;
            } else if (falling) {
                falling = false;
                gameObject.layer = 8;
                //rb2d.simulated = false;
                //rb2d.simulated = true;
                fallTime = 0.0f;
            }
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = 0;
        if (leftClick) moveHorizontal--;
        if (rightClick) moveHorizontal++;

        Vector2 movement = new Vector2(moveHorizontal * status.speed, rb2d.velocity.y);
        rb2d.velocity = movement;

        if (jump)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, status.jumpForce));

            jump = false;
            grounded = false;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("platform") || other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("ground"))
        {
            grounded = true;
            doubleJump = true;
        }

        if (other.gameObject.CompareTag("bullet"))
        {
            if (other.gameObject.name.Substring(0, 8) != bulletPrefab.name.Substring(0, 8))
            {
                status.health -= (other.gameObject.GetComponent<BulletStatus>().bulletDamage-GetComponent<PlayerStatus>().armor);
                Destroy(other.gameObject);
            }
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
            bulletVelocity = -status.bullet_speed;
        else
            bulletVelocity = status.bullet_speed;

        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletVelocity, 0);
        bullet.GetComponent<BulletStatus>().bulletDamage = status.bullet_damage;
        bullet.GetComponent<BulletStatus>().lifespan = 2.0f;
    }
}
