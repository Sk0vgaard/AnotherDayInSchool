using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HealthSystem : MonoBehaviour
{
    public int healthAmount;
    public int maxHealth;
    [HideInInspector]
    public bool isDead;

    public void Awake()
    {
        GameObject healthBar = Resources.Load("HealthBar") as GameObject;
        GameObject instance = Instantiate(healthBar, transform.position, transform.rotation) as GameObject;
        instance.transform.SetParent(gameObject.transform);
        instance.SetActive(true);
        // setting the character one the healthbar since when the healthbar is instansiated it doesnt have any parent
        instance.GetComponent<HealthBar>().character = this;

    }

    // Use this for initialization
    public void Start ()
	{
	    healthAmount = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {

	}

    public abstract void Die(); 

    public void TakeDamage(int damageTaken)
    {
        if (isDead)
        {
            return;
        }

        healthAmount -= damageTaken;
        if (healthAmount <= 0)
        {
            healthAmount = 0;
            Die();
        }
    }
}
