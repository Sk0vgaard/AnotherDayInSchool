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

    public void ActivateEnemies()
    {
        foreach (var enemy in enemies)
        {
            enemy.player = player;
        }
    }

    public void DeactivateEnemies()
    {
        foreach (var enemy in enemies)
        {
            enemy.player = null;
        }
    }
}
