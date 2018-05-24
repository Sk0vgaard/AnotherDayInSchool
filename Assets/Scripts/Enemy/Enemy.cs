using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : HealthSystem {

    public PlayerController player;

    private Room room;

    new public void Awake()
    {
        base.Awake();
		if (transform.parent != null) {
			GameObject parent = transform.parent.gameObject;
			if (parent != null) {
				Debug.Log("Parent: " + parent);
				Debug.Log(gameObject.name);
				room = transform.parent.gameObject.GetComponent<Room>();
			}
		}
       

        
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

    public abstract void Activate(PlayerController player);
    public abstract void Deactivate(PlayerController player);


}
