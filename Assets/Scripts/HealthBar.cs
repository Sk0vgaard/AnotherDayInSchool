using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Vector3 localScale;
    private HealthSystem character;
    private GameObject redBar;

    void Awake()
    {
        character = transform.parent.GetComponent<HealthSystem>();
        redBar = transform.parent.Find("HealthBar Red").gameObject;
    }

    // Use this for initialization
    void Start ()
	{
	    localScale.x = (float)character.healthAmount / 40f;
        localScale = transform.localScale;
	    redBar.transform.localScale = new Vector2(2.5f, 0.3f);
	}
	
	// Update is called once per frame
	void Update ()
	{
	    localScale.x = (float)character.healthAmount / 40f;
	    transform.localScale = localScale;
	}

    
}
