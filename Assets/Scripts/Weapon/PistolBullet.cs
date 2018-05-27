using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolBullet : AProjectile {


    private GameObject smokeEffect;
    

    new void Awake()
    {
        base.Awake();
        smokeEffect = Resources.Load("ProjectileHitEffect") as GameObject;
    }

    // Use this for initialization
    new void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	new void Update () {
        base.Update();
	}

    public override void Hit(Collider2D collider)
    {
        if (collider.tag == "Terrain")
        {
            // smokeEffect
            Instantiate(smokeEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        // Collides with an object with healthSystem.
        if (collider.GetComponent<HealthSystem>() != null)
        {
            HealthSystem hp = collider.GetComponent<HealthSystem>();
            if (!(owner == hp.gameObject))
            {
                hp.TakeDamage(damage);
                Instantiate(smokeEffect, transform.position, transform.rotation);
                Destroy(gameObject);
            }        
        }
    }
}
