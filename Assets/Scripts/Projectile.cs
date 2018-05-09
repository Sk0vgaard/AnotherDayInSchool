using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public int damage;


    public void Awake()
    {
        
    }
    // Use this for initialization
    public void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public abstract void OnHit();

    private void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.tag == "Terrain")
        {
            OnHit();
        }

        if (collider.GetComponent<HealthSystem>() != null)
        {
            //player.TakeDamage();
            HealthSystem hp = collider.GetComponent<HealthSystem>();
            hp.TakeDamage(damage);
            OnHit();
        }

    }
}
