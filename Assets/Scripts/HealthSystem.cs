using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HealthSystem : MonoBehaviour
{
    private Rigidbody2D rb;
    private int healthAmount;
    public int maxHealth;

	// Use this for initialization
	void Start ()
	{
	    healthAmount = maxHealth;
	    rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

	}

    void TakeDamage(int damageTaken)
    {
        healthAmount -= damageTaken;
        if (healthAmount <= 0)
        {
            Destroy(gameObject);
        }
    }
}
