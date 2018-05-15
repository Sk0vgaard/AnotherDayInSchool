using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : HealthSystem {

    public PlacyerController player;

    private Room room;

    new public void Awake()
    {
        base.Awake();
        GameObject parent = transform.parent.gameObject;
        Debug.Log("Parent: " + parent);
        Debug.Log(gameObject.name);

        room = transform.parent.gameObject.GetComponent<Room>();
    }

	// Use this for initialization
	new void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	void Update () {
	    	
	}

    public override void Die()
    {
        if (room != null)
        {
            room.EnemyKilled(this);
        }
    }

}
