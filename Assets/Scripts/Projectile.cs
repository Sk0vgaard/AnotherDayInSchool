using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public int damage;


	// Use this for initialization
	public void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.tag == "Terrain")
        {
            Destroy(gameObject);
        }

        if (collider.GetComponent<HealthSystem>() != null)
        {
<<<<<<< HEAD
            PlacyerController player = collider.GetComponent<PlacyerController>();


            //player.TakeDamage();
=======
            HealthSystem hp = collider.GetComponent<HealthSystem>();
            hp.TakeDamage(damage);
>>>>>>> bd8864c29374e0454ddfff0af81b4b4948164c51
            Destroy(gameObject);

        }

    }
}
