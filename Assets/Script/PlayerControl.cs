﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public bool activated = false;

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
    public float bullet_rate_count;

    public GameObject hitFXPrefab;

    public AudioClip hitAudio;
    public AudioClip shootAudio;
    public AudioClip stepAudio;
    public AudioClip jumpAudio;
    private bool grounded = false;

    //public bool IsPlayerOne;
    private bool falling = false;
    private float fallTime = 0.0f;
    private const float MIN_FALL_TIME = 0.4f;

    void Start()
    {
        //rb2d = GetComponent<Rigidbody2D>();
        //freeze the rotation of the object
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        //getting the playerstatus 
        status = GetComponent<PlayerStatus>();
        GetComponent<AudioSource>().playOnAwake = false;

        bullet_rate_count = 0;
    }

    void Update()
    {
        if (Input.GetKey("escape"))
        {
            SceneManager.LoadScene("HotPizzaTitle");
        }
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

            // drop from platform
            if (Input.GetKeyDown(downButton)) {
                falling = true;
                grounded = false;
                jump = false;
                gameObject.layer = 9;
                fallTime = 0.0f;
                fallTime += Time.deltaTime;
            } else if (falling && fallTime < MIN_FALL_TIME) {
                fallTime += Time.deltaTime;
            } else if (falling) {
                falling = false;
                gameObject.layer = 8;
                fallTime = 0.0f;
            }
        }
    }

    void FixedUpdate()
    {
        //shooting depends on the fire rate
        if (bullet_rate_count <= 0)
        {
            if (shootClick && activated)
            {
                Fire();
                bullet_rate_count = status.bullet_fire_rate;
            }
        }
        else
        {
            bullet_rate_count -= Time.deltaTime;
        }

        float moveHorizontal = 0;
        if (leftClick)
        {
            GetComponent<AudioSource>().clip = stepAudio;
            GetComponent<AudioSource>().Play();
            moveHorizontal--;
        }
            
        if (rightClick)
        {
            GetComponent<AudioSource>().clip = stepAudio;
            GetComponent<AudioSource>().Play();
            moveHorizontal++;
        }
        Vector2 movement = new Vector2(moveHorizontal * status.speed, rb2d.velocity.y);
        rb2d.velocity = movement;

        if (jump)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, status.jumpForce));
            GetComponent<AudioSource>().PlayOneShot(jumpAudio);
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
                status.health -= other.gameObject.GetComponent<BulletStatus>().bulletDamage;
                Destroy(other.gameObject);
                GetComponent<AudioSource>().PlayOneShot(hitAudio);
                var hitFX = (GameObject)Instantiate(
                    hitFXPrefab,
                    gameObject.transform.position,
                    gameObject.transform.rotation);
            }
        }
    }

    void Fire()
    {
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);
        GetComponent<AudioSource>().PlayOneShot(shootAudio);
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
