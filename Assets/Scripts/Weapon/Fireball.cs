using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : AProjectile {

    private GameObject smokeEffect;

    new void Awake()
    {
        base.Awake();
        smokeEffect = Resources.Load("ProjectileHitEffect") as GameObject;
    }
	
	// Update is called once per frame
	new void Update () {

        // Overrides super.class update. (Uses another rotation).
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    /// <summary>
    /// On collides/hit
    /// </summary>
    /// <param name="collider"></param>
    public override void Hit(Collider2D collider)
    {
        if (collider.tag == "Terrain")
        {
            Instantiate(smokeEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        // Collides with an object with healthSystem.
        if (collider.GetComponent<HealthSystem>() != null)
        {
            HealthSystem hp = collider.GetComponent<HealthSystem>();
            if (!(owner == hp.gameObject))
            {
                hp.TakeDamage(damage);
                Instantiate(smokeEffect, transform.position, Quaternion.identity);
            }
        }
    }
}
