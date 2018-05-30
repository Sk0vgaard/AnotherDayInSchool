using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookBullet : AProjectile {

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
        // When collides with terrain, destroy the projectile.
        if (collider.tag == "Terrain")
        {
            Destroy(gameObject);
            Instantiate(smokeEffect, transform.position, Quaternion.identity);

        }

        HitObjectsWithHealthSystem(collider);
    }

    /// <summary>
    /// Characters, enemies and so on.
    /// </summary>
    /// <param name="collider"></param>
    private void HitObjectsWithHealthSystem(Collider2D collider)
    {
        if (collider.GetComponent<HealthSystem>() != null)
        {
            // Lose hp.
            HealthSystem hp = collider.GetComponent<HealthSystem>();
            if (!(owner == hp))
            {
                // Take damage and destroy book bullet.
                hp.TakeDamage(damage);
                Destroy(gameObject);
                Instantiate(smokeEffect, transform.position, Quaternion.identity);

            }
        }
    }
}
