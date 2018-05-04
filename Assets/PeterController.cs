using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Internal;
using UnityEngine;

public class PeterController : MonoBehaviour
{

    public float moveSpeed;

    private Animator anim;
    private static readonly float COUNTER = 0.42f;
    private static readonly float MOVE_THRESHHOLD = 0.3f;

    private GameObject bookBullet;
    private GameObject target;



	void Start () {
        target = FindObjectOfType<PlacyerController>().gameObject;
	}
	
	void Update () {

	    if (Input.GetKeyDown(KeyCode.Space))
	    {
	        StartCoroutine(ThrowBook());
	    }

        if (target == null)
        {
            target = FindObjectOfType<PlacyerController>().gameObject;
        }

        if (! (transform.position.y >= target.transform.position.y - MOVE_THRESHHOLD && transform.position.y <= target.transform.position.y + MOVE_THRESHHOLD))
        {
            FollowPlayer();

        }
    }

    void Awake ()
    {
        anim = transform.Find("Peter").GetComponent<Animator>();
        bookBullet = Resources.Load("BookBullet") as GameObject;
    }

    IEnumerator ThrowBook()
    {
        anim.SetTrigger("attackTrigger");
        yield return new WaitForSeconds(COUNTER);
        Instantiate(bookBullet, transform.position, transform.rotation);
        transform.Find("Peter").Find("Hand L Pivot").Find("Hand L").Find("UnityBook").gameObject.SetActive(false);
        yield return new WaitForSeconds(COUNTER);
        transform.Find("Peter").Find("Hand L Pivot").Find("Hand L").Find("UnityBook").gameObject.SetActive(true);
    }


    void FollowPlayer()
    {
        if (PlayerIsAbove())
        {
            //go up
            transform.Translate(Vector2.up * Time.deltaTime * moveSpeed);
        }
        else
        {
            //go down
            transform.Translate(Vector2.down * Time.deltaTime * moveSpeed);

        }
    }

    bool PlayerIsAbove()
    {
        if (target)
        {
            if (target.transform.position.y > transform.position.y)
            {
                return true;
            }
            
        }
        return false;
    }
}
