using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLifeSpan : MonoBehaviour
{

    private ParticleSystem psys;

    public void Start()
    {
        psys = GetComponent<ParticleSystem>();
    }

    public void Update()
    {
        if (psys)
        {
            if (!psys.IsAlive())
            {
                Destroy(gameObject);
            }
        }
    }
}
