using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDeflectorAttack : MonoBehaviour {

    private List<AProjectile> projectiles;
    [HideInInspector]
    public AEnemy owner;

    private void Awake()
    {
        projectiles = new List<AProjectile>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<AProjectile>())
        {
            // Sets the speed of any projectiles hitting the collider to 0, and adds the projectile to the projectiles list. 
            AProjectile projectile = other.GetComponent<AProjectile>();
            projectile.speed = 0;
            projectile.transform.parent = transform;
            projectiles.Add(projectile);
        }
    }
    

    public void RevertProjectiles()
    {
        //Rotates all projectiles in the projectiles list by 180 and sets their speed to their original speed.
        foreach (var projectile in projectiles)
        {
            if (projectile )
            {
                projectile.transform.parent = null;
                projectile.transform.localRotation = Quaternion.Euler(0, 0, projectile.transform.rotation.eulerAngles.z + 180);
                projectile.speed = projectile.originalSpeed * 1;
                projectile.owner = null;
            }
            
        }
        // Destory the shield
        Destroy(gameObject);
    }
}
