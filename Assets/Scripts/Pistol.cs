using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon {

    public float fireRate;
    public GameObject projectile;
    public GameObject muzzleFlare;

    public Transform spawnPoint;
    public Transform muzzleFlareSpawnPoint;

    private float fireCooldown;
    private Animator animator;
    private SpriteRenderer sr;

    new void Awake()
    {
        base.Awake();
        fireCooldown = 0;
        projectile = Resources.Load("Bullet") as GameObject;
        muzzleFlare = Resources.Load("MuzzleFlare") as GameObject;

        spawnPoint = transform.Find("ProjectileSpawnPoint");
        muzzleFlareSpawnPoint = transform.Find("MuzzleFlareSpawnPoint");
        animator = transform.Find("Sprite").GetComponent<Animator>();
        sr = transform.Find("Sprite").GetComponent<SpriteRenderer>(); ;

    }

    private void Update()
    {
        if (isPressingTrigger)
        {
            if (fireCooldown  <= 0)
            {
                Fire();
                fireCooldown = fireRate;
            }
        }

        if (fireCooldown > 0)
        {
            fireCooldown -= Time.deltaTime;
        }
        else
        {
            fireCooldown = 0;
        }
    }

    void Fire()
    {
        if (sr.flipY == true)
        {
            animator.SetTrigger("FireTrigger");
        }
        else
        {
            animator.SetTrigger("FireTriggerFlipped");
        }

        Instantiate(muzzleFlare, muzzleFlareSpawnPoint.position, transform.rotation);
        
        Instantiate(projectile, spawnPoint.position, transform.rotation);
    }
}
