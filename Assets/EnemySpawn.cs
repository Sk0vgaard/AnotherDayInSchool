using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {
	private GameObject enemy;
	private bool rdyToSpawn = true;
	public Transform sp1, sp2;

	// Use this for initialization
	void Start () {
		enemy = Resources.Load ("WalkingMob") as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (rdyToSpawn) {
			StartCoroutine (SpawnMob());
		}
	}

	IEnumerator SpawnMob() {
		rdyToSpawn = false;
		float randomX = Random.Range (sp1.position.x, sp2.position.x);
		Vector2 vector = new Vector2 (randomX, sp1.position.y);
		float randomSpeed = Random.Range (2, 8);
		enemy.GetComponent<MobWalkStraightLine> ().speed = randomSpeed;
		Instantiate (enemy, vector, Quaternion.identity);
		yield return new WaitForSeconds(1);
		rdyToSpawn = true;
	}
}
