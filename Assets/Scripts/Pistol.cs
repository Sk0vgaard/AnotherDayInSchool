using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon {

    public float fireRate;
    private float fireCooldown;
    public GameObject projectile;
    public GameObject muzzleFlare;

    public Transform spawnPoint;
    public Transform muzzleFlareSpawnPoint;


    new void Awake()
    {
        base.Awake();
        fireCooldown = 0;
        projectile = Resources.Load("Bullet") as GameObject;
        muzzleFlare = Resources.Load("MuzzleFlare") as GameObject;

        spawnPoint = transform.Find("ProjectileSpawnPoint");
        muzzleFlareSpawnPoint = transform.Find("MuzzleFlareSpawnPoint");


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
        Instantiate(muzzleFlare, muzzleFlareSpawnPoint.position, transform.rotation);

        Instantiate(projectile, spawnPoint.position, transform.rotation);
    }
}
