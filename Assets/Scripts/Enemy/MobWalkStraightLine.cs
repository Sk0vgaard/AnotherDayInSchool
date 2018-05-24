using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobWalkStraightLine : AEnemy {
	public float speed = 3f;

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

    public override void Activate(PlayerController player)
    {
    }

    public override void Deactivate(PlayerController player)
    {
    }
}
