using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HealthSystem : MonoBehaviour
{
    [HideInInspector]
    public int healthAmount;
    public int maxHealth;



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

    public void TakeDamage(int damageTaken)
    {
        healthAmount -= damageTaken;
        if (healthAmount <= 0)
        {
            Destroy(gameObject);
        }
    }
}
