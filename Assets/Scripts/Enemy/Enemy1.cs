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
        base.Die();
        isDead = true;
        Destroy(gameObject);
    }

    public override void Activate(PlayerController player)
    {
        this.player = player;
    }

    public override void Deactivate(PlayerController player)
    {
        this.player = null;
    }
}
