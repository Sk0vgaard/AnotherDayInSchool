using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardRoom : ARoom {

    public override void Enter(PlayerController player)
    {
        ActivateEnemies(player);
        isPlayerInRoom = true; 
    }

    public override void Exit()
    {
        DeactivateEnemies();
        isPlayerInRoom = false;

    }
}
