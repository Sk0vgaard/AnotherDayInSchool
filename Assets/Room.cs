using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Room : MonoBehaviour {

    public List<Enemy> enemies;
    public bool roomClearOfEnemies;

    public void Awake()
    {
        enemies = new List<Enemy>();
        GetReferenceToEnemiesInRoom();
        
        
    }

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

    public void GetReferenceToEnemiesInRoom()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            if (child.GetComponent<Enemy>())
            {
                enemies.Add(child.GetComponent<Enemy>());
            }
        }

        if (enemies.Count <= 0)
        {
            roomClearOfEnemies = true;
        }
    }

    /// <summary>
    /// Remove enemy from list of enemies. Keeping track of enemies to check if the room is cleared
    /// </summary>
    /// <param name="enemy"></param>
    public void EnemyKilled(Enemy enemy)
    {
        enemies.Remove(enemy);
        if (enemies.Count <= 0)
        {
            roomClearOfEnemies = true;
        }
    }
}
