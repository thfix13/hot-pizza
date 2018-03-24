using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour {

    public int life;
    public float health;
    public float bullet_damage;
    public float bullet_speed;
    public float defend;
    public float speed;
    public float jumpForce;

    public bool canDoubleJump;

	void Update () {
        if (health < 0)
            gameObject.SetActive(false);
	}
}
