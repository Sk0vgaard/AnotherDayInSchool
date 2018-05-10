using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Enemy {



    // Use this for initialization
    new void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void Die()
    {
        isDead = true;
        Destroy(gameObject);
    }
}
