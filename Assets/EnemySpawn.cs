using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {
	private GameObject enemy;
	private PlacyerController player;
	private bool rdyToSpawn = true;
	public Transform sp1, sp2;

	// Use this for initialization
	void Start () {
		enemy = Resources.Load ("WalkingMob") as GameObject;
		player = FindObjectOfType<PlacyerController> ();
		Debug.Log ("Player: " + player);
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
		float randomSpeed = Random.Range (2, 10);
		enemy.GetComponent<MobWalkStraightLine> ().speed = randomSpeed;
		GameObject enemy1 = Instantiate (enemy, vector, Quaternion.identity) as GameObject;
		enemy1.GetComponent<Enemy> ().player = player;
		yield return new WaitForSeconds (0.3f);
		rdyToSpawn = true;
	}
}
