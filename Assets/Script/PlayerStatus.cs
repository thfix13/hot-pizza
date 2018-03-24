using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{

    public int life;
    public float fullhealth;
    public float health;
    public float health_regen;
    public float bullet_damage;
    public float bullet_speed;
    public float armor;
    public float speed;
    public float jumpForce;
    public GameObject deathParticlePrefab;
    public bool canDoubleJump;
    public Vector2Int loadoutSelection;
    public float deathTime = 1f;
    private float deathTimeRemain;
    private int selector;
    private GameObject[] allBullets;

    void Awake()
    {
        deathTimeRemain = 0;
        armor = 0;
        health_regen = 0;
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
            }
            if(life==1)
            {
                AddPowerUp(2);
            }
            health = fullhealth;
            allBullets = GameObject.FindGameObjectsWithTag("bullet");
            for (var i = 0; i < allBullets.Length; i++)
            {
                Destroy(allBullets[i]);
            }

            gameObject.SetActive(true);
        }
    }

    private void AddPowerUp(int a)
    {
        //To differentiate between first and second death
        if (a == 1) selector = loadoutSelection.x;
        else selector = loadoutSelection.y;

        //Movement power up
        if (selector== 1)
        {
            switch (Random.Range(1, 2))
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
            switch (Random.Range(1, 2))
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
            switch (Random.Range(1, 2))
            {
                case 1:
                    //Reduce damage received
                    Debug.Log("3,1", gameObject);
                    armor = 10;
                    break;
                case 2:
                    //Grants health regeneration
                    Debug.Log("3,2", gameObject);
                    health_regen = 0.1f;
                    break;
            }
        }
    }
    
}
