using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour {




	// Use this for initialization
	void Start () {
		
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

        if (collider.GetComponent<PlacyerController>() != null)
        {
            PlacyerController player = collider.GetComponent<PlacyerController>();
            //player.TakeDamage();
            Destroy(gameObject);

        }

    }
}
