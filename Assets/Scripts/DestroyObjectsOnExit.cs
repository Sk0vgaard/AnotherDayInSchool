using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectsOnExit : MonoBehaviour {

    private ARoom room;

	// Use this for initialization
	void Start () {
        room = transform.parent.GetComponent<ARoom>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.GetComponent<AEnemy>())
        {
            AEnemy aEnemy = collision.gameObject.GetComponent<AEnemy>();
            if (room.enemies.Contains(aEnemy))
            {
                room.enemies.Remove(aEnemy);
            }
            room.enemies.Remove(aEnemy);

            aEnemy.Die();
            Destroy(aEnemy.gameObject);
        }

    }
}
