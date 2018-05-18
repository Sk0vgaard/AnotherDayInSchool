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
    public bool disabled;
    private PlacyerController player;
    private bool walkPlayerToCenter, walkPlayerAwayFromCenter;
    private Animator canvasAnimator;


    private void Awake()
    {
        GameObject canvas = FindObjectOfType<PauseMenu>().gameObject;
        if (canvas != null)
        {
            canvasAnimator = canvas.transform.Find("FadeToBlackImage").GetComponent<Animator>();
        }
    }

    // Use this for initialization
    void  Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
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
                //player.disableMovements = false;
                player = null;
            }
        }

        if (walkPlayerAwayFromCenter)
        {
            Vector2 playerPos = player.transform.position;
            if (Vector2.Distance(playerPos, transform.position) < 2)
            {
                Vector2 direction = Vector2.zero;
                switch (exitDirection)
                {
                    case exitDir.up: direction = Vector2.up; break;
                    case exitDir.left: direction = Vector2.left; break;
                    case exitDir.down: direction = Vector2.down; break;
                    case exitDir.right: direction = Vector2.right; break;
                }
                player.transform.Translate(direction * Time.deltaTime * player.character.moveSpeed * 0.75f);
            }
            else
            {
                walkPlayerAwayFromCenter = false;
                player.disableMovements = false;
                room.Enter(player);
                player = null;

            }

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.GetComponent<PlacyerController>() != null)
        {
            if (disabled)
            {
                if (canvasAnimator)
                {
                    canvasAnimator.SetBool("Black", false);
                }
                player = other.GetComponent<PlacyerController>();
                walkPlayerAwayFromCenter = true;
            }
            else
            {
                player = other.GetComponent<PlacyerController>();
                if (room.roomClearOfEnemies)
                {
                    WalkThroughDoor();
                }
            }           
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlacyerController>() != null)
        {
            if (disabled)
            {
                disabled = false;
            }
        }
    }

    void WalkThroughDoor()
    {
        if (canvasAnimator)
        {
            canvasAnimator.SetBool("Black",true);
        }
        walkPlayerToCenter = true;
        player.disableMovements = true;
        room.Exit();
        exit.disabled = true;
    }
}
