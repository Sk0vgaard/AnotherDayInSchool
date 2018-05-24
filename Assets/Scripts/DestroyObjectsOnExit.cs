using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectsOnExit : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject);

        if (collision.gameObject.GetComponent<Enemy>())
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.Die();
            Destroy(enemy.gameObject);
        }

    }
}
