using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour {

    public GameObject poi;

	// Use this for initialization
	void Start () {
        poi = FindObjectOfType<PlayerController>().gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        if (poi)
        {
            transform.position = new Vector3(poi.transform.position.x, poi.transform.position.y, -10);
            //transform.position.Set();// poi.transform.position.x;
        }
    }
}
