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
    public bool shootClick = false;
    [HideInInspector]
    public bool activated = true;

    //button define for this player
    public KeyCode jumpButton;
    public KeyCode leftButton;
    public KeyCode rightButton;
    public KeyCode shootButton;

    //user define player values
    public Rigidbody2D rb2d;
    private PlayerStatus status;
    public bool faceLeft;

    //user define bullet values
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    private float bullet_fire_count;

    private bool grounded = false;

    void Start()
    {
        //rb2d = GetComponent<Rigidbody2D>();
        //freeze the rotation of the object
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        //getting the playerstatus 
        status = GetComponent<PlayerStatus>();

        bullet_fire_count = 0;
    }

    void Update()
    {
        if (activated)
        {
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
            if (Input.GetKeyDown(shootButton)) shootClick = true;
            if (Input.GetKeyUp(shootButton)) shootClick = false;

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

        //shooting bullet according to fire rate
        if (bullet_fire_count <= 0)
        {
            if (shootClick)
            {
                Fire();
                bullet_fire_count = status.fire_rate;
            }
        }
        else
        {
            bullet_fire_count -= Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("platform") || other.gameObject.CompareTag("Player"))
        {
            grounded = true;
            doubleJump = true;
        }

        if (other.gameObject.CompareTag("bullet"))
        {
            if (other.gameObject.name.Substring(0, 8) != bulletPrefab.name.Substring(0, 8))
            {
                status.health -= other.gameObject.GetComponent<BulletStatus>().bulletDamage;
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
