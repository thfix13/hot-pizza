using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public bool isP1;
    public int life;
    public float fullhealth;
    public float health;
    public float health_regen;
    public float bullet_damage;
    public float bullet_speed;
    public float bullet_fire_rate;
    public float armor;
    public float speed;
    public float jumpForce;
    public GameObject deathParticlePrefab;
    public GameObject movementParticlePrefab;
    public GameObject attackParticlePrefab;
    public GameObject defenseParticlePrefab;
    public bool canDoubleJump;
    public Vector2Int loadoutSelection;
    public float deathTime = 1f;
    public AudioClip powerUpSound;
    private float deathTimeRemain;
    private int selector;
    private GameObject[] allBullets;

    public Sprite firstForm;
    public Sprite secondForm;
    public Sprite finalForm;

    void Awake()
    {
        deathTimeRemain = 0;
        armor = 0;
        health_regen = 0;
        if(isP1)loadoutSelection = Selection.P1selection;
        else loadoutSelection = Selection.P2selection;
        GetComponent<AudioSource>().playOnAwake = false;
    }

    void Update()
    {
        health += health_regen;
        if (health <= 0)
        {
            gameObject.SetActive(false);
            var deathParticles = (GameObject)Instantiate(
                deathParticlePrefab,
                gameObject.transform.position,
                gameObject.transform.rotation);
            life--;
            if(life==2)
            {
                AddPowerUp(1);
                GetComponent<SpriteRenderer>().sprite = secondForm;
            }
            if(life==1)
            {
                AddPowerUp(2);
                GetComponent<SpriteRenderer>().sprite = finalForm;
            }
            health = fullhealth;
            allBullets = GameObject.FindGameObjectsWithTag("bullet");
            for (var i = 0; i < allBullets.Length; i++)
            {
                Destroy(allBullets[i]);
            }

            gameObject.SetActive(true);
            GetComponent<AudioSource>().clip = powerUpSound;
            GetComponent<AudioSource>().Play();
        }
    }

    private void AddPowerUp(int a)
    {
        //To differentiate between first and second death
        if (a == 1) selector = loadoutSelection.x;
        else selector = loadoutSelection.y;

        //Movement power up
        if (selector == 1)
        {
            var movementParticles = (GameObject)Instantiate(
                movementParticlePrefab,
                gameObject.transform.position,
                gameObject.transform.rotation);
            movementParticles.transform.parent = gameObject.transform;
            switch (Random.Range(1, 3))
            {
                case 1:
                    //Increased jump height
                    Debug.Log("1,1", gameObject);
                    jumpForce += 500;
                    break;
                case 2:
                    //Increased speed
                    Debug.Log("1,2", gameObject);
                    speed *= 1.2f;
                    break;
            }
        }
        //Attack power up
        else if (selector == 2)
        {
            var attackParticles = (GameObject)Instantiate(
                attackParticlePrefab,
                gameObject.transform.position,
                gameObject.transform.rotation);
            attackParticles.transform.parent = gameObject.transform;
            switch (Random.Range(1, 3))
            {
                case 1:
                    //Increased bullet damage
                    Debug.Log("2,1", gameObject);
                    bullet_damage += 10;
                    break;
                case 2:
                    //Increased bullet speed
                    Debug.Log("2,2", gameObject);
                    bullet_speed *= 1.2f;
                    break;
            }
        }
        //Defense power up
        else
        {
            var defenseParticles = (GameObject)Instantiate(
                defenseParticlePrefab,
                gameObject.transform.position,
                gameObject.transform.rotation);
            defenseParticles.transform.parent = gameObject.transform;
            switch (Random.Range(1, 3))
            {
                case 1:
                    //Reduce damage received
                    Debug.Log("3,1", gameObject);
                    armor += 5;
                    break;
                case 2:
                    //Grants health regeneration
                    Debug.Log("3,2", gameObject);
                    health_regen += 0.1f;
                    break;
            }
        }
    }
    
}
