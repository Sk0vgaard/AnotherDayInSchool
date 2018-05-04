using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Internal;
using UnityEngine;

public class PeterController : MonoBehaviour
{
    private Animator anim;
    //private float counter;
    private static readonly float COUNTER = 0.42f;
    private GameObject bookBullet;
    //private bool countDown;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	    if (Input.GetKeyDown(KeyCode.Space))
	    {
	        StartCoroutine(ThrowBook());
	        //Attack();
	    }
        
	}

    void Awake ()
    {
        anim = GetComponent<Animator>();
        bookBullet = Resources.Load("BookBullet") as GameObject;
    }

    IEnumerator ThrowBook()
    {

        anim.SetTrigger("attackTrigger");
    
        yield return new WaitForSeconds(COUNTER);

        Instantiate(bookBullet, transform.position, transform.rotation);
        transform.Find("Hand L Pivot").Find("Hand L").Find("UnityBook").gameObject.SetActive(false);

        yield return new WaitForSeconds(COUNTER);

        transform.Find("Hand L Pivot").Find("Hand L").Find("UnityBook").gameObject.SetActive(true);

    }
}
