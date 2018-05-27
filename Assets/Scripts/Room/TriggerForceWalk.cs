using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerForceWalk : MonoBehaviour {

    public Transform destination;
    public BossRoom1 room;
    private bool movePlayer;
    private PlayerController player;

	void Start () {
		
	}
	
	void Update ()
	{
	    MovePlayerToPosition();
	}

    /// <summary>
    /// Moves the player to the wished position/destination.
    /// </summary>
    private void MovePlayerToPosition()
    {
        if (movePlayer)
        {
            if (player != null)
            {
                if (Vector2.Distance(destination.position, player.transform.position) > 0.1f)
                {
                    Vector3 dir = destination.position -
                                  new Vector3(player.transform.position.x, player.transform.position.y, 0);
                    player.transform.Translate(dir.normalized * Time.deltaTime * player.character.moveSpeed * 0.75f);
                }
                else
                {
                    movePlayer = false;
                    player.disableMovements = false;
                    room.StartFight(player);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<PlayerController>() != null)
        {
            PlayerController player = collider.GetComponent<PlayerController>();
            WalkPlayerTODestination(player);
        }
    }

    void WalkPlayerTODestination(PlayerController player)
    {
        player.disableMovements = true;
        movePlayer = true;
        this.player = player; 
    }
}
