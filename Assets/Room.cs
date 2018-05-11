using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Room : MonoBehaviour {


    public List<Enemy> enemies;

    

    public abstract void Enter(PlacyerController player);
    public abstract void Exit();

    public void ActivateEnemies(PlacyerController player)
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
