using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossRoom1 : ABossRoom
{
    PlayerController player;
    public BlockDoorObject blockDoor;

    private bool runOnce = true;

    public new void Awake()
    {
        base.Awake();

    }

    public void Update()
    {
        if (roomClearOfEnemies && runOnce)
        {
            runOnce = false;
            FindObjectOfType<NextLevel>().NewLevel();
        }
    }

    

    /// <summary>
    /// When player enters the room.
    /// </summary>
    /// <param name="player"></param>
    public override void Enter(PlayerController player)
    {
        base.Enter(player);
        this.player = player;
        // The player shouldnt be able to go out of the room.
        if (!blockDoor.IsOpen())
        {
            blockDoor.Open();
        }
        isPlayerInRoom = true;
        

    }

    public override void Exit()
    {
        base.Exit();

        this.player = null;
        isPlayerInRoom = false;
    
    }

    /// <summary>
    /// When the fight agianst the boss starts, lock the door.
    /// </summary>
    /// <param name="player"></param>
    public void StartFight(PlayerController player)
    {
        //Activate boss.
        ActivateEnemies(player);

        if (blockDoor.IsOpen())
        {
            blockDoor.Close();

        }
    }

    


}
