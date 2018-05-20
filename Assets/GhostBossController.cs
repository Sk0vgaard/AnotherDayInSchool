using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GhostBossController : Enemy {

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
    private LookAt lookAtPlayer;
    private bool deflectBullets;
    private GameObject deflectorResource;
    public bool atCenter;

    public float speed;
    

    new void Awake()
    {
        base.Awake();
        readyForRoutine = true;
        fireBallResource = Resources.Load("Fireball") as GameObject;
        deflectorResource = Resources.Load("ProjectileDeflector") as GameObject;

        lookAtPlayer = transform.Find("LookAtPlayerObject").GetComponent<LookAt>();
        
    }

    // Use this for initialization
    new void Start () {
        base.Start();
        startingPosition = transform.position;
        

    }

    // Update is called once per frame
    void Update () {

        AtCenter();
        if (isDead)
        {
            StopAllCoroutines();
            state = State.NoMovement;
        }
        if (player != null)
        {
            switch (state)
            {
                case State.FollowPlayer: FollowPlayer();
                    break;
                case State.WalkToCenter: MovePlayerToCenter();
                    break;
                case State.NoMovement: break;
            }
        }
        
	}

    void FollowPlayer()
    {
        Vector2 dir = player.transform.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime);
    }   

    void MovePlayerToCenter()
    {
        Vector2 dir = startingPosition - transform.position;
        if (!(transform.position.y >= startingPosition.y - MOVE_THRESHHOLD && transform.position.y <= startingPosition.y + MOVE_THRESHHOLD))
        {
            transform.Translate(dir.normalized * speed * Time.deltaTime);
        }
    }

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
        fireball.GetComponent<Projectile>().owner = gameObject;
    }

    ProjectileDeflectorAttack SpawnDeflector()
    {
        GameObject deflectorInstance = Instantiate(deflectorResource, lookAtPlayer.transform.position, Quaternion.identity) as GameObject;
        ProjectileDeflectorAttack deflector = deflectorInstance.GetComponent<ProjectileDeflectorAttack>();
        deflector.owner = gameObject.GetComponent<Enemy>();
        deflector.transform.SetParent(transform);
        return deflector;
    }

    public override void Activate(PlacyerController player)
    {
        this.player = player;
        lookAtPlayer.poi = player.gameObject;
        
        StartCoroutine(Phase1());
        
    }

    public override void Deactivate(PlacyerController player)
    {
        this.player = null;
        lookAtPlayer.poi = null;
    }

    public override void Die()
    {
        base.Die();
        isDead = true;
    }


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
        yield return new WaitForSeconds(2f);


        StartCoroutine(Phase1());
    }

    IEnumerator Routine1()
    {
        state = State.FollowPlayer;
        yield return new WaitForSeconds(2.5f);

        state = State.NoMovement;
        //speed = 10;

        StartCoroutine(WalkToCenter());
    }

    IEnumerator WalkToCenter()
    {
        state = State.WalkToCenter;
        yield return new WaitUntil(() => atCenter == true);
        state = State.NoMovement;
        StartCoroutine(ShootFireballs());
    }

    IEnumerator ShootFireballs()
    {
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < 4; i++)
        {
            SpawnFireBall();
            yield return new WaitForSeconds(0.4f);
        }

        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Routine1());
    }

    

}


