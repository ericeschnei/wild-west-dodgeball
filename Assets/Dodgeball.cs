using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodgeball : MonoBehaviour
{
    public ParticleSystem trail;

    private bool active = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (active)
        {
            PlayerHealth ph = other.collider.GetComponentInParent<PlayerHealth>();
            if (ph)
            {
                ph.TakeDamage(Mathf.Infinity);
            }
            active = false;
            trail.Stop();
            GetComponent<Rigidbody2D>().drag = 2;
        }
        
    }
}
