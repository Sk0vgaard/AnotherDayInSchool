using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom1 : Room
{
    PlacyerController player;
    public BlockDoorObject blockDoor;

    public override void Enter(PlacyerController player)
    {
        this.player = player;
        if (!blockDoor.IsOpen())
        {
            blockDoor.Open();

        }
    }

    public override void Exit()
    {
        this.player = null;
    }

    public void StartFight(PlacyerController player)
    {
        ActivateEnemies(player);
        if (blockDoor.IsOpen())
        {
            blockDoor.Close();
        }
    }


}
