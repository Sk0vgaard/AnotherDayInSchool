using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardRoom : Room {

    public override void Enter(PlayerController player)
    {
        ActivateEnemies(player);
    }

    public override void Exit()
    {
        DeactivateEnemies();
    }
}
