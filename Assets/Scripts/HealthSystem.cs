using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HealthSystem : MonoBehaviour
{
    [HideInInspector]
    public int healthAmount;
    public int maxHealth;

	// Use this for initialization
	public void Start ()
	{
	    healthAmount = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void TakeDamage(int damageTaken)
    {
        Debug.Log(damageTaken + " " + healthAmount);
        healthAmount -= damageTaken;
        if (healthAmount <= 0)
        {
            Destroy(gameObject);
        }
    }
}
