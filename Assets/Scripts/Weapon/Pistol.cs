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

    private AudioSource fire;

    new void Awake()
    {
        base.Awake();
        fireCooldown = 0;

        GetReferences();
    }

    /// <summary>
    /// Gets the references (Should make an interface).
    /// </summary>
    private void GetReferences()
    {
        projectile = Resources.Load("Bullet") as GameObject;
        muzzleFlare = Resources.Load("MuzzleFlare") as GameObject;

        spawnPoint = transform.Find("ProjectileSpawnPoint");
        muzzleFlareSpawnPoint = transform.Find("MuzzleFlareSpawnPoint");
        animator = transform.Find("Sprite").GetComponent<Animator>();
        sr = transform.Find("Sprite").GetComponent<SpriteRenderer>();

        fire = GetComponent<AudioSource>();
    }

    private void Update()
    {
        Shoot();
    }

    /// <summary>
    /// When player shoots.
    /// </summary>
    private void Shoot()
    {
        // When holding trigger down.
        if (isPressingTrigger)
        {
            if (fireCooldown <= 0)
            {
                Fire();
                fireCooldown = fireRate;
            }
        }

        // Cooldown system so player isnt able to spam click.
        if (fireCooldown > 0)
        {
            fireCooldown -= Time.deltaTime;
        }
        else
        {
            fireCooldown = 0;
        }
    }

    /// <summary>
    /// Instantiate bullet, muzzleFlare and sound.
    /// </summary>
    void Fire()
    {
        FireAnimation();

        Instantiate(muzzleFlare, muzzleFlareSpawnPoint.position, transform.rotation);

        // Fire sound
        fire.Play();
        
        GameObject projectileGameObject = Instantiate(projectile, spawnPoint.position, transform.rotation) as GameObject;
        AProjectile projectileInstance = projectileGameObject.GetComponent<AProjectile>();

        // The character who holds the gun.
        if (holder != null)
        {
            projectileInstance.owner = holder.gameObject;
        }
    }


    private void FireAnimation()
    {
        if (sr.flipY)
        {
            animator.SetTrigger("FireTrigger");
        }
        else
        {
            animator.SetTrigger("FireTriggerFlipped");
        }
    }
}
