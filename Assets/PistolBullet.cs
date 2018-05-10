﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolBullet : Projectile {


    private GameObject smokeOffect;
    

    new void Awake()
    {
        base.Awake();
        smokeOffect = Resources.Load("ProjectileHitEffect") as GameObject;
    }

    // Use this for initialization
    new void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void Hit(Collider2D collider)
    {
        if (collider.tag == "Terrain")
        {
            Instantiate(smokeOffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        if (collider.GetComponent<HealthSystem>() != null)
        {
            //player.TakeDamage();
            HealthSystem hp = collider.GetComponent<HealthSystem>();
            if (!(owner == hp.gameObject))
            {
                hp.TakeDamage(damage);
                Instantiate(smokeOffect, transform.position, transform.rotation);
                Destroy(gameObject);
            }        
        }
    }
}
