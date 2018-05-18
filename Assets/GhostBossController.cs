using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GhostBossController : Enemy {

    private enum State
    {
        FollowPlayer,
        WalkToCenter,
    }

    private State state;
    private Vector3 startingPosition;
    private bool readyForRoutine, readyForRoutine2;
    private static readonly float MOVE_THRESHHOLD = 0.3f;
    private GameObject fireBallResource;
    private LookAt lookAtPlayer;

    public float speed;
    

    new void Awake()
    {
        base.Awake();
        readyForRoutine = true;
        fireBallResource = Resources.Load("Fireball") as GameObject;
        lookAtPlayer = transform.Find("LookAtPlayerObject").GetComponent<LookAt>();
        
    }

    // Use this for initialization
    new void Start () {
        base.Start();
        startingPosition = transform.position;
        if (readyForRoutine)
        {
            StartCoroutine(Routine1());
        }

    }

    // Update is called once per frame
    void Update () {

        if (player != null)
        {
            switch (state)
            {
                case State.FollowPlayer: FollowPlayer();
                    break;
                case State.WalkToCenter: WalkToCenter();
                    break;
            }
        }
        
	}

    void FollowPlayer()
    {
        Vector2 dir = player.transform.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime);
    }   

    void WalkToCenter()
    {
        Vector2 dir = startingPosition - transform.position;
        if (!(transform.position.y >= startingPosition.y - MOVE_THRESHHOLD && transform.position.y <= startingPosition.y + MOVE_THRESHHOLD))
        {
            transform.Translate(dir.normalized * speed * Time.deltaTime);
        }
        else
        {
            if (readyForRoutine2)
            {
                StartCoroutine(ShootFireballs());

            }
        }
    }

    void SpawnFireBall()
    {
        Debug.Log("Spawning fireball");
        GameObject fireball = Instantiate(fireBallResource, lookAtPlayer.transform.position, lookAtPlayer.transform.rotation) as GameObject;
        fireball.GetComponent<Projectile>().owner = gameObject;
    }

    IEnumerator Routine1()
    {
        SpawnFireBall();

        speed = 5;
        state = State.FollowPlayer;
        yield return new WaitForSeconds(4);
        state = State.WalkToCenter;
        speed = 10;
        readyForRoutine2 = true;
        
    }

    IEnumerator ShootFireballs()
    {
        readyForRoutine2 = false;

        for (int i = 0; i < 5; i++)
        {
            SpawnFireBall();
            yield return new WaitForSeconds(0.6f);
        }
        StartCoroutine(Routine1());
    }

    public override void Activate(PlacyerController player)
    {
        this.player = player;
        lookAtPlayer.poi = player.gameObject;
    }

    public override void Deactivate(PlacyerController player)
    {
        this.player = null;
        lookAtPlayer.poi = null;

    }
}


