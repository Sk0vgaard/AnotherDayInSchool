using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeterController : MonoBehaviour
{
    private Animator anim;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.Space))
	    {
	        Attack();
	    }
	}

    void Awake ()
    {
        anim = GetComponent<Animator>();
    }

    
    void Attack ()
    {
        anim.SetTrigger("attackTrigger");    
    }
}
