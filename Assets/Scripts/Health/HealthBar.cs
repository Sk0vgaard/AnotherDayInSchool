using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Vector3 localScale;
    public HealthSystem character;
    private GameObject redBar;
    private GameObject greenBar;

    public float HEALTH_BAR_SIZE = 4;


    void Awake()
    {
        redBar = transform.Find("RedBar").gameObject;
        greenBar = transform.Find("GreenBar").gameObject;


    }

    // Use this for initialization
    void Start ()
    {
        localScale.x = (float) character.healthAmount / character.startingHealth * HEALTH_BAR_SIZE;

        greenBar.transform.localScale = new Vector2(localScale.x, 0.3f);
        redBar.transform.localScale = new Vector2(localScale.x, 0.3f);
        greenBar.transform.position = new Vector2(transform.position.x + greenBar.transform.localScale.x / -2, transform.position.y+1);
        redBar.transform.position = new Vector2(transform.position.x + redBar.transform.localScale.x / -2, transform.position.y+1);

    }

    // Update is called once per frame
    void Update ()
	{
        localScale.x = (float)character.healthAmount / character.startingHealth * HEALTH_BAR_SIZE;

        //localScale.x = (float)character.healthAmount / 40f;
        greenBar.transform.localScale = new Vector2(localScale.x, 0.3f);
	}

    
}
