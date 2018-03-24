using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{

    public int life;
    public float fullhealth;
    public float health;
    public float bullet_damage;
    public float bullet_speed;
    public float fire_rate;
    public float defend;
    public float speed;
    public float jumpForce;
    public GameObject deathParticlePrefab;
    public bool canDoubleJump;

    public float deathTime = 1f;
    private float deathTimeRemain;

    void Awake()
    {
        deathTimeRemain = 0;
    }

    void Update()
    {
        if (health <= 0)
        {
            gameObject.SetActive(false);

            var deathParticles = (GameObject)Instantiate(
                deathParticlePrefab,
                gameObject.transform.position,
                gameObject.transform.rotation);
            life--;
            health = fullhealth;

            gameObject.SetActive(true);
        }
    }
}
