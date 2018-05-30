using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ARoom : MonoBehaviour {

    public List<AEnemy> enemies;
    public bool roomClearOfEnemies;
    public bool isPlayerInRoom;
    public bool isStartingRoom;

    protected AudioSource bgm;

    public void Awake()
    {
        enemies = new List<AEnemy>();
        GetReferenceToEnemiesInRoom();
        bgm = GameObject.Find("BGM").GetComponent<AudioSource>();
    }

    public void Start()
    {
        if (isStartingRoom)
        {
            PlayerController player = FindObjectOfType<PlayerController>();
            Enter(player);
        }
    }

    public abstract void Enter(PlayerController player);
    public abstract void Exit();

    public void ActivateEnemies(PlayerController player)
    {
        foreach (var enemy in enemies)
        {
            enemy.Activate(player);
        }
    }

    public void DeactivateEnemies()
    {
        foreach (var enemy in enemies)
        {
            enemy.Deactivate(null);
        }
    }

    public void GetReferenceToEnemiesInRoom()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            if (child.GetComponent<AEnemy>())
            {
                enemies.Add(child.GetComponent<AEnemy>());
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
    /// <param name="aEnemy"></param>
    public void EnemyKilled(AEnemy aEnemy)
    {
        int sizeb = enemies.Count;

        bool x = enemies.Remove(aEnemy);

        Debug.Log("Size: " + enemies.Count + " before: " + sizeb);

        if (enemies.Count <= 0)
        {
            roomClearOfEnemies = true;
        }
    }
}
