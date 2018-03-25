using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletStatus : MonoBehaviour {

    [HideInInspector]
    public float bulletDamage;
    [HideInInspector]
    public float lifespan;
    //might be useful in the future
    [HideInInspector]
    private float speed;
    [Range(0.0f, 100.0f)]
    public float rpm = 60.0f;
    
    void Update()
    {
        lifespan -= Time.deltaTime;
        transform.Rotate(new Vector3(0, 0, rpm * 6.0f) * Time.deltaTime);
    }

    void LateUpdate()
    {
        if (lifespan < 0)
            Destroy(gameObject);
    }

    // Destroy on hitting a wall or the ground
    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("ground") || other.gameObject.CompareTag("wall")) {
            Destroy(gameObject);
        }
    }
}
