using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AEnemy : HealthSystem {

    public PlayerController player;

    private ARoom room;

    new public void Awake()
    {
        base.Awake();

        // If enemy has a parent.
		if (transform.parent != null) {
			GameObject parent = transform.parent.gameObject;
            // Get the reference to the parent.
			if (parent != null) {
				room = transform.parent.gameObject.GetComponent<ARoom>(); // Can only be used in rooms atm.
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

    /// <summary>
    /// Tells the room that the enemy is dead.
    /// </summary>
    public override void Die()
    {
        if (room != null)
        {
            Debug.Log(gameObject + " is dead");
            room.EnemyKilled(GetComponent<AEnemy>());
        }
    }

    public abstract void Activate(PlayerController player);
    public abstract void Deactivate(PlayerController player);
}
