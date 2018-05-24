using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDeflectorAttack : MonoBehaviour {

    private List<Projectile> projectiles;
    //private float counter;
    //public float counterStartingTime;
    [HideInInspector]
    public AEnemy owner;

    private void Awake()
    {
        //projectiles = new List<Projectile>();
        //counter = counterStartingTime;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Projectile>())
        {
            Projectile projectile = other.GetComponent<Projectile>();
            //projectile.speed = 0;
            //projectile.transform.parent = transform;
            //projectiles.Add(projectile);

            projectile.transform.localRotation = Quaternion.Euler(0, 0, projectile.transform.rotation.eulerAngles.z + 180);
            projectile.owner = null;
        }
    }



    
    public void RevertProjectiles()
    {
        /*
        foreach (var projectile in projectiles)
        {
            projectile.transform.parent = null;
            projectile.transform.localRotation = Quaternion.Euler(0,0, projectile.transform.rotation.eulerAngles.z + 180);
            projectile.speed = projectile.originalSpeed * 1;
            projectile.owner = null;
        }*/
        Destroy(gameObject);
    }
}
