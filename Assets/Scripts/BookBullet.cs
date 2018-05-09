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

    public override void OnHit()
    {
        Destroy(gameObject);

    }
}
