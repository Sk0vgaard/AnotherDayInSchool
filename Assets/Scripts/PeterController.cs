﻿using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PeterController : Enemy
{
    [SerializeField] private string newLevel;

    public float moveSpeed;

    private Animator anim;
    private static readonly float COUNTER = 0.42f;
    private static readonly float MOVE_THRESHHOLD = 0.3f;

    private GameObject bookBullet;
    private HealthSystem target;
    private Transform bookSpawnPoint;

    private bool readyToStartCoroutine;


    void Awake()
    {
        base.Awake();
        anim = transform.Find("Peter").GetComponent<Animator>();
        bookBullet = Resources.Load("BookBullet") as GameObject;
        bookSpawnPoint = transform.Find("Peter").Find("Hand L Pivot").Find("Hand L").Find("BookSpawnPoint");

    }

    new void Start () {
        base.Start();
        readyToStartCoroutine = true;
        target = FindObjectOfType<PlacyerController>().GetComponent<HealthSystem>();
	}

    void Update()
    {
        if (isDead)
        {
            SceneManager.LoadScene(newLevel);
        }

        if (player)
        {
            target = player;
        }
        else
        {
            target = null;
        }

        /*if (target == null)
        {
            PlacyerController playerTarget = FindObjectOfType<PlacyerController>();
            if (playerTarget != null)
            {
                target = playerTarget.GetComponent<HealthSystem>();
            }
        }*/
        if (target != null)
        {
            if (!target.isDead)
            {
                if (!(transform.position.y >= target.transform.position.y - MOVE_THRESHHOLD && transform.position.y <= target.transform.position.y + MOVE_THRESHHOLD))
                {
                    FollowPlayer();
                }

                if (readyToStartCoroutine)
                {
                    StartCoroutine(PeterRoutine());
                }
            }
            
        }
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

    IEnumerator PeterRoutine()
    {
        readyToStartCoroutine = false;

        StartCoroutine(ThrowBook());
        yield return new WaitForSeconds(1.5f);

        readyToStartCoroutine = true;
    }

    void InstantiateBook()
    {
        GameObject bookGameObject = Instantiate(bookBullet, bookSpawnPoint.position, transform.rotation) as GameObject;
        bookGameObject.GetComponent<Projectile>().owner = gameObject;

    }

    IEnumerator ThrowBook()
    {
        anim.SetTrigger("attackTrigger");
        yield return new WaitForSeconds(COUNTER);
        InstantiateBook();
        transform.Find("Peter").Find("Hand L Pivot").Find("Hand L").Find("UnityBook").gameObject.SetActive(false);
        yield return new WaitForSeconds(COUNTER);
        transform.Find("Peter").Find("Hand L Pivot").Find("Hand L").Find("UnityBook").gameObject.SetActive(true);
    }

    public override void Die()
    {
        base.Die();
        isDead = true;
    }

}
