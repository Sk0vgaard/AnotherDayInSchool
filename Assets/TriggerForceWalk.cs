using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerForceWalk : MonoBehaviour {

    public Transform destination;
    public EnemiesActivateOnEventRoom room;
    private bool movePlayer;
    private PlacyerController player;

	void Start () {
		
	}
	
	void Update () {
        if (movePlayer)
        {
            if (player != null)
            {
                if (Vector2.Distance(destination.position,player.transform.position) > 0.1f)
                {
                    Vector3 dir = destination.position - new Vector3(player.transform.position.x, player.transform.position.y, 0);
                    player.transform.Translate(dir.normalized * Time.deltaTime * player.character.moveSpeed * 0.75f);
                }
                else
                {
                    movePlayer = false;
                    player.disableMovements = false;
                    Debug.Log("ONCE");
                    room.ActivateEnemies(player);

                }
            }            
        }
	}

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<PlacyerController>() != null)
        {
            PlacyerController player = collider.GetComponent<PlacyerController>();
            WalkPlayerTODestination(player);
        }
    }

    void WalkPlayerTODestination(PlacyerController player)
    {
        player.disableMovements = true;
        movePlayer = true;
        this.player = player; 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
