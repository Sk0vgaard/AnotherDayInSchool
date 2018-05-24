using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PeterController : AEnemy
{
    public float moveSpeed;

    private Animator anim;
    private static readonly float COUNTER = 0.42f;
    private static readonly float MOVE_THRESHHOLD = 0.3f;


    private GameObject bookBullet;
    private Transform bookSpawnPoint;



    new void Awake()
    {
        base.Awake();
        anim = transform.Find("Peter").GetComponent<Animator>();    //Get reference to peters animationsController.
        bookBullet = Resources.Load("BookBullet") as GameObject;    //Get reference to peters book.
        //Get reference where the book should spawn.
        bookSpawnPoint = transform.Find("Peter").Find("Hand L Pivot").Find("Hand L").Find("BookSpawnPoint");
    }

    new void Start () {
        base.Start();
    }

    void Update()
    {
        if (!isDead)
        {
            FollowPlayer();
        }
    }

    /// <summary>
    /// Follow the player.
    /// </summary>
    private void FollowPlayer()
    {
        if (player != null)
        {
            if (!player.isDead)
            {
                // Space to move in.
                if (!(transform.position.y >= player.transform.position.y - MOVE_THRESHHOLD && transform.position.y <=
                      player.transform.position.y + MOVE_THRESHHOLD))
                {
                    GoUpOrDown();
                }
            }
        }
    }

    /// <summary>
    /// Checks if the payer is above or under the boss.
    /// </summary>
    void GoUpOrDown()
    {
        if (IsPlayerAbove())
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

    /// <summary>
    /// Checks if the player is higher on the y aksis.
    /// </summary>
    /// <returns></returns>
    bool IsPlayerAbove()
    {
            if (player.transform.position.y > transform.position.y)
            {
                return true;
            }
            return false;
    }


    void InstantiateBook()
    {
        GameObject bookGameObject = Instantiate(bookBullet, bookSpawnPoint.position, transform.rotation) as GameObject;
        // Sets the protectile owner to be the boss, so you cannot damage yourself.
        bookGameObject.GetComponent<Projectile>().owner = gameObject;
    }

    public override void Die()
    {
        anim.SetTrigger("deathTrigger");
        base.Die();
        StopAllCoroutines();
        isDead = true;
    }

    /// <summary>
    /// Starts when you go into the room.
    /// </summary>
    /// <param name="player"></param>
    public override void Activate(PlayerController player)
    {
        StartCoroutine(PeterRoutine());

        this.player = player;
    }

    /// <summary>
    /// When your not in the room or the boss is dead.
    /// </summary>
    /// <param name="player"></param>
    public override void Deactivate(PlayerController player)
    {
        this.player = null;
    }


    /// <summary>
    /// Peters rutine.
    /// </summary>
    /// <returns></returns>
    IEnumerator PeterRoutine()
    {
        StartCoroutine(ThrowBook());
        yield return new WaitForSeconds(1.5f);

        // Loop all the way.
        StartCoroutine(PeterRoutine());
    }

    /// <summary>
    /// Animation routine.
    /// </summary>
    /// <returns></returns>
    IEnumerator ThrowBook()
    {
        anim.SetTrigger("attackTrigger");
        yield return new WaitForSeconds(COUNTER);
        InstantiateBook();
        transform.Find("Peter").Find("Hand L Pivot").Find("Hand L").Find("UnityBook").gameObject.SetActive(false);
        yield return new WaitForSeconds(COUNTER);
        transform.Find("Peter").Find("Hand L Pivot").Find("Hand L").Find("UnityBook").gameObject.SetActive(true);
    }
}
