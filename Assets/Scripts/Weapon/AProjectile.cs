using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AProjectile : MonoBehaviour
{
    public int damage;
    public GameObject owner;
    public float speed;
    [HideInInspector]
    public float originalSpeed;


    public void Awake()
    {
        
    }

    public void Start () {
        originalSpeed = speed;
	}
	
	// Update is called once per frame
	public void Update () {

        // Makes the projectile move.
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    /// <summary>
    /// When projectiles hit/collides.
    /// </summary>
    /// <param name="collider"></param>
    public abstract void Hit(Collider2D collider);


    private void OnTriggerEnter2D(Collider2D collider)
    {
        Hit(collider);
    }
}
