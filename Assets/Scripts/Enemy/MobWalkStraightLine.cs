using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobWalkStraightLine : AEnemy {
	public float speed = 3f;
    private 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector2.down * speed * Time.deltaTime);
	}

	void OnCollisionStay2D(Collision2D col)
	{
		if (col.gameObject.GetComponent<PlayerController>())
			col.gameObject.GetComponent<PlayerController>().TakeDamage(100000);
	}

    // Activates the enemy when player walks into the room.
    public override void Activate(PlayerController player)
    {
        
    }

    // Deactivates when player is not in the room.
    public override void Deactivate(PlayerController player)
    {
        Destroy(gameObject);
    }
}
