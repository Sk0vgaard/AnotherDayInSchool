using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum exitDir
{
    up,
    left,
    down,
    right
}

public class Door : MonoBehaviour {


    public Room room;
    public exitDir exitDirection;
    public Door exit;
    public bool doorIsDisabled;
    private PlayerController player;
    private bool walkPlayerToCenter, walkPlayerAwayFromCenter;
    private Animator canvasAnimator;


    private void Awake()
    {
        InitializeFadeEffect();
    }

    private void InitializeFadeEffect()
    {
        GameObject canvas = FindObjectOfType<PauseMenu>().gameObject;
        if (canvas != null)
        {
            canvasAnimator = canvas.transform.Find("FadeToBlackImage").GetComponent<Animator>();
        }
    }
	
	// Update is called once per frame
	void Update ()
	{
	    WalkIntoTheDoor();

	    WalkFromTheDoor();
	}

    /// <summary>
    /// When the player walks from the door (2-3 steps when arriving to a new room).
    /// </summary>
    private void WalkFromTheDoor()
    {
        if (walkPlayerAwayFromCenter)
        {
            Vector2 playerPos = player.transform.position;

            // Tells the character what direction to move from the door.
            if (Vector2.Distance(playerPos, transform.position) < 2)
            {
                Vector2 direction = Vector2.zero;
                switch (exitDirection)
                {
                    case exitDir.up:
                        direction = Vector2.up;
                        break;
                    case exitDir.left:
                        direction = Vector2.left;
                        break;
                    case exitDir.down:
                        direction = Vector2.down;
                        break;
                    case exitDir.right:
                        direction = Vector2.right;
                        break;
                }
                // Moves the player to the wished direction.
                player.transform.Translate(direction * Time.deltaTime * player.character.moveSpeed * 0.75f);
            }
            // When the player done walking through the door, make the player able to move agian.
            else
            {
                walkPlayerAwayFromCenter = false;
                player.disableMovements = false;

                // Tells the new room that the player has arrived.
                room.Enter(player);
                player = null;
            }
        }
    }

    /// <summary>
    /// Make the player walk deeper into the door, but with slower movespeed.
    /// </summary>
    private void WalkIntoTheDoor()
    {
        if (walkPlayerToCenter)
        {
            Vector2 playerPos = player.transform.position;
            if (Vector2.Distance(playerPos, transform.position) > 0.1f)
            {
                Vector3 dir = transform.position - new Vector3(playerPos.x, playerPos.y, 0);
                player.transform.Translate(dir * Time.deltaTime * player.character.moveSpeed * 0.75f);
            }
            else
            {
                player.transform.position = exit.transform.position;
                walkPlayerToCenter = false;
                player = null;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If door collides with the player.
        if (other.GetComponent<PlayerController>() != null)
        {
            // Checks if the door is locked.
            if (doorIsDisabled)
            {
                // If door is locked, play fadeout animation
                if (canvasAnimator)
                {
                    canvasAnimator.SetBool("Black", false);
                }
                player = other.GetComponent<PlayerController>();
                walkPlayerAwayFromCenter = true;
                
            }
            else
            {
                // If the door has a exit. (Not currently).
                if (exit)
                {
                    player = other.GetComponent<PlayerController>();

                    // Checks if all enemies id died to lock up the door.
                    if (room.roomClearOfEnemies)
                    {
                        WalkThroughDoor();
                    }
                }
                
            }           
        }
    }

    /// <summary>
    /// When going out from the doors collider.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            if (doorIsDisabled)
            {
                // Open door.
                doorIsDisabled = false;
            }
        }
    }

    /// <summary>
    /// Fade in when walking through the door.
    /// </summary>
    void WalkThroughDoor()
    {
        if (canvasAnimator)
        {
            canvasAnimator.SetBool("Black",true);
        }
        walkPlayerToCenter = true;
        player.disableMovements = true;
        room.Exit(); // To deactivate the enemies.
        exit.doorIsDisabled = true; // Wont be thrown back in pre room.
    }
}
