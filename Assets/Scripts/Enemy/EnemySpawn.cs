using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {
	private GameObject enemyResource;
	private PlayerController player;
	private bool rdyToSpawn = true;
    private ARoom room;

    public Transform sp1, sp2;
    public float spawnDelay;

	// Use this for initialization
	void Start () {
        if (GetComponent<ARoom>())
        {
            room = GetComponent<ARoom>();
        }
        enemyResource = Resources.Load ("WalkingMob") as GameObject;
		player = FindObjectOfType<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (rdyToSpawn && room.isPlayerInRoom) {
			StartCoroutine (SpawnMob());
		}
	}

    /// <summary>
    /// Spawn the mobs.
    /// </summary>
    /// <returns></returns>
	IEnumerator SpawnMob() {
		rdyToSpawn = false;
		float randomX = Random.Range (sp1.position.x, sp2.position.x);
		Vector2 vector = new Vector2 (randomX, sp1.position.y);
		float randomSpeed = Random.Range (2, 10);
		enemyResource.GetComponent<MobWalkStraightLine> ().speed = randomSpeed;
        //Spawns the enemy
		GameObject enemeyGameObject = Instantiate (enemyResource, vector, Quaternion.identity) as GameObject;
        //Sets the enemies parent to the room
        enemeyGameObject.transform.parent = transform;
     
        AEnemy enemy = enemeyGameObject.GetComponent<AEnemy>();
        enemy.player = player;
        room.enemies.Add(enemy);
	
        yield return new WaitForSeconds (spawnDelay);
		rdyToSpawn = true;
	}
}
