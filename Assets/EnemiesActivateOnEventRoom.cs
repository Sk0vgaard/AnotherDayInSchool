using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesActivateOnEventRoom : Room
{
    PlacyerController player;

    public override void Enter(PlacyerController player)
    {
        this.player = player;

    }

    public override void Exit()
    {
        this.player = null;
    }

    
}
