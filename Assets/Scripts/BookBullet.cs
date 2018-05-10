using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookBullet : Projectile {
    

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
            Destroy(gameObject);
        }

        if (collider.GetComponent<HealthSystem>() != null)
        {
            //player.TakeDamage();
            HealthSystem hp = collider.GetComponent<HealthSystem>();
            if (!(owner == hp.gameObject))
            {
                hp.TakeDamage(damage);
                Destroy(gameObject);
            }

        }

    }
}
