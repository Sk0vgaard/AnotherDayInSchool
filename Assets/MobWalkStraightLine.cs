using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobWalkStraightLine : Enemy {
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
		if (col.gameObject.GetComponent<PlacyerController>())
			col.gameObject.GetComponent<PlacyerController>().TakeDamage(100000);
	}

    public override void Activate(PlacyerController player)
    {
    }

    public override void Deactivate(PlacyerController player)
    {
    }
}
