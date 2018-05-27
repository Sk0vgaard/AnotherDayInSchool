using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mobs : AEnemy {
    public float speed = 3f;
    private GameObject blood;
    


    // Use this for initialization
    new void Start ()
    {
        base.Start();
    }

    new void Awake()
    {
        base.Awake();
        blood = Resources.Load("blood") as GameObject;

    }

    // Update is called once per frame
    void Update () {
        //If player is alive.
	    if (!isDead && player != null)
	    {
	        transform.LookAt(player.transform.position);
	        transform.Rotate(new Vector3(0, -90, 0), Space.Self); //correcting the original rotation


	        // Move towards the player.
	            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }

    }

    public override void Die()
    {
        base.Die();
        isDead = true;
        Instantiate(blood, transform.position, transform.rotation);
        Destroy(gameObject);

    }

    /// <summary>
    /// When the mobs hits the player.
    /// </summary>
    /// <param name="col"></param>
    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<PlayerController>())
        col.gameObject.GetComponent<PlayerController>().TakeDamage(1);
    }

    // Activates the enemy when player walks into the room.
    public override void Activate(PlayerController player)
    {
        this.player = player;
    }

    // Deactivates when player is not in the room.
    public override void Deactivate(PlayerController player)
    {
        this.player = null;
    }
}
