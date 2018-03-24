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
    
    void Update()
    {
        lifespan -= Time.deltaTime;
    }

    void LateUpdate()
    {
        if (lifespan < 0)
            Destroy(gameObject);
    }
}
