using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mobs : Enemy {
    public float speed = 3f;
    private GameObject blood;
    


    // Use this for initialization
    new void Start ()
    {
        base.Start();
    }
	
	// Update is called once per frame
	void Update () {
	    if (!isDead && player != null)
	    {
	        transform.LookAt(player.transform.position);
	        transform.Rotate(new Vector3(0, -90, 0), Space.Self);//correcting the original rotation


	        //move towards the player
	        if (Vector3.Distance(transform.position, player.transform.position) > 1f)
	        {
	            //move if distance from target is greater than 1
	            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
	        }
	        else
	        {
	            // Do damage to player
	            
	        }

        }
        //rotate to look at the player

    }

    public override void Die()
    {
        base.Die();
        isDead = true;
        Instantiate(blood, transform.position, transform.rotation);
        Destroy(gameObject);

    }

    new void Awake()
    {
        base.Awake();
        blood = Resources.Load("blood") as GameObject;
        
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<PlacyerController>())
        col.gameObject.GetComponent<PlacyerController>().TakeDamage(1);
    }
}
