using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GhostBossController : AEnemy {

    private enum State
    {
        FollowPlayer,
        WalkToCenter,
        NoMovement
    }

    private State state;
    private Vector3 startingPosition;
    private bool readyForRoutine, readyForRoutine2;
    private static readonly float MOVE_THRESHHOLD = 0.3f;
    private GameObject fireBallResource;
    private LookAtObject lookAtPlayer;
    private bool deflectBullets;
    private GameObject deflectorResource;
    private Animator anim;

    public bool atCenter;

    public float speed;
    

    new void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
        readyForRoutine = true;
        fireBallResource = Resources.Load("Fireball") as GameObject;
        deflectorResource = Resources.Load("ProjectileDeflector") as GameObject;

        lookAtPlayer = transform.Find("LookAtPlayerObject").GetComponent<LookAtObject>();
    }

    // Use this for initialization
    new void Start () {
        base.Start();
        startingPosition = transform.position; //Where the boss spawn.
    }

    // Update is called once per frame
    void Update ()
    {
        AtCenter();
    }

    void FixedUpdate()
    {
        HandleBossState();
    }

    /// <summary>
    /// The state of the boss.
    /// </summary>
    private void HandleBossState()
    {
        if (player != null)
        {
            switch (state)
            {
                case State.FollowPlayer:
                    FollowPlayer();
                    break;
                case State.WalkToCenter:
                    MoveBossToCenter();
                    break;
                case State.NoMovement: break;
            }
        }
    }

    /// <summary>
    /// Follow the player.
    /// </summary>
    void FollowPlayer()
    {
        Vector2 dir = player.transform.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime);
    }   

    /// <summary>
    /// Moves the boss back to the starting point.
    /// </summary>
    void MoveBossToCenter()
    {
        Vector2 dir = startingPosition - transform.position;
        if (!(transform.position.y >= startingPosition.y - MOVE_THRESHHOLD && transform.position.y <= startingPosition.y + MOVE_THRESHHOLD))
        {
            transform.Translate(dir.normalized * speed * Time.deltaTime);
        }
    }

    /// <summary>
    /// Figures out if the boss is in center or not.
    /// </summary>
    void AtCenter()
    {
        if (!(transform.position.y >= startingPosition.y - MOVE_THRESHHOLD && transform.position.y <= startingPosition.y + MOVE_THRESHHOLD))
        {
            atCenter = false;
        }
        else
        {
            atCenter = true;
        }
    }

 
    void SpawnFireBall()
    {
        GameObject fireball = Instantiate(fireBallResource, lookAtPlayer.transform.position, lookAtPlayer.transform.rotation) as GameObject;
        fireball.GetComponent<AProjectile>().owner = gameObject.GetComponent<HealthSystem>();
    }

    /// <summary>
    /// Makes the shield to deflect the projectiles.
    /// </summary>
    /// <returns></returns>
    ProjectileDeflectorAttack SpawnDeflector()
    {
        GameObject deflectorInstance = Instantiate(deflectorResource, lookAtPlayer.transform.position, Quaternion.identity) as GameObject;
        ProjectileDeflectorAttack deflector = deflectorInstance.GetComponent<ProjectileDeflectorAttack>();
        deflector.owner = gameObject.GetComponent<AEnemy>();
        deflector.transform.SetParent(transform);
        return deflector;
    }

    public override void Activate(PlayerController player)
    {
        Debug.Log("Player:" + player);
        this.player = player;
        lookAtPlayer.poi = player.gameObject;
        
        StartCoroutine(Phase1());
        
    }

    public override void Deactivate(PlayerController player)
    {
        this.player = null;
        lookAtPlayer.poi = null;
    }

    public override void Die()
    {
        base.Die();
        anim.SetTrigger("deathTrigger");

        StopAllCoroutines();
        isDead = true;
        state = State.NoMovement;
    }

    /// <summary>
    /// Routine for the boss.
    /// </summary>
    /// <returns></returns>
    IEnumerator Phase1()
    {
        for (int i = 0; i < 3; i++)
        {
            state = State.FollowPlayer;
            yield return new WaitForSeconds(2.5f);
            state = State.NoMovement;

            yield return new WaitForSeconds(0.5f);
            for (int j = 0; j < 4; j++)
            {
                SpawnFireBall();
                yield return new WaitForSeconds(0.4f);
            }
            yield return new WaitForSeconds(0.5f);
        }

        state = State.WalkToCenter;
        yield return new WaitUntil(() => atCenter == true);
        state = State.NoMovement;
        ProjectileDeflectorAttack deflector = SpawnDeflector();
        deflector.owner = GetComponent<AEnemy>();
        
        yield return new WaitForSeconds(3f);
        deflector.RevertProjectiles();
        yield return new WaitForSeconds(0.2f);


        StartCoroutine(Phase1());
    }
}


