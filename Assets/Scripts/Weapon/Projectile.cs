using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public int damage;
    public GameObject owner;
    public float speed;
    [HideInInspector]
    public float originalSpeed;

    public void Awake()
    {
        
    }
    // Use this for initialization
    public void Start () {
        originalSpeed = speed;
	}
	
	// Update is called once per frame
	public void Update () {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

    }

    public abstract void Hit(Collider2D collider);

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Hit(collider);
    }

    public void StandardHitMethod(Collider2D collider)
    {
        if (collider.tag == "Terrain")
        {
            DestroyObject(gameObject);
        }

        if (collider.GetComponent<HealthSystem>() != null)
        {
            //player.TakeDamage();
            HealthSystem hp = collider.GetComponent<HealthSystem>();
            hp.TakeDamage(damage);
            DestroyObject(gameObject);
        }
    }
}
