using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selfdestruct : MonoBehaviour {

    public float startingTime;
    private float time;

    private void Awake()
    {
        time = startingTime;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        time -= Time.deltaTime;

        if (time <= 0)
        {
            Destroy(gameObject);
        }

	}
}
