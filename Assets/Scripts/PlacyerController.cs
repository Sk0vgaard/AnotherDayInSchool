using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacyerController : MonoBehaviour {

    public Character character;
    public Animator animator;

    private Vector2 moveDirection;
    private Rigidbody2D rb;
    private SpriteRenderer sr;


    private void Awake()
    {
        character.Initialize();
        rb = GetComponent<Rigidbody2D>();
        sr = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        animator = transform.Find("Sprite").GetComponent<Animator>();
        animator.runtimeAnimatorController = character.animatorController;
    }

    void Start () {

        //animator.SetFloat("speed", 10.0f);
    }
	
	void Update () {
        moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        if (rb.velocity.x == 0 && rb.velocity.y == 0)
        {
            animator.SetFloat("speed", 0f);

        }
        else
        {
            animator.SetFloat("speed", 1.0f);
        }


        if (rb.velocity.x < 0)
        {
            sr.flipX = true;
        }
        else if (rb.velocity.x > 0)
        {
            sr.flipX = false;
        }
    }

    void FixedUpdate()
    {
        rb.velocity = moveDirection * character.moveSpeed;
    }
}
