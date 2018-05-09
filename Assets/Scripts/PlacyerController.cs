using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacyerController : HealthSystem {

    public Character character;
    public Animator animator;

    public bool disableMovements;

    private Vector2 moveDirection;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Weapon currentWeapon;
    private bool flipped;


    new void Awake()
    {
        base.Awake();        
        character.Initialize();
        rb = GetComponent<Rigidbody2D>();
        sr = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        animator = transform.Find("Sprite").GetComponent<Animator>();
        animator.runtimeAnimatorController = character.animatorController;
    }

    new void Start () {
        base.Start();
        GameObject weaponGameObject = Instantiate(character.startingWeapon.gameObject, transform.position, transform.rotation) as GameObject;
        weaponGameObject.transform.SetParent(this.transform);
        currentWeapon = weaponGameObject.GetComponent<Weapon>();
        
    }
	
	void Update () {
        if (rb.velocity.x == 0 && rb.velocity.y == 0)
        {
            animator.SetFloat("speed", 0f);

        }
        else
        {
            animator.SetFloat("speed", 1.0f);
        }


        if (rb.velocity.x < 0)
        {
            sr.flipX = true;
        }
        else if (rb.velocity.x > 0)
        {
            sr.flipX = false;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            disableMovements = !disableMovements;
        }

        if (!disableMovements)
        {
            Movement();
            HandleShooting();
        }
        else
        {
            moveDirection = new Vector2(0,0);
            if (currentWeapon != null)
            {
                currentWeapon.isPressingTrigger = false;
            }
        }
    }

    void Movement()
    {
        moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }

    void HandleShooting()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (currentWeapon != null)
            {
                currentWeapon.isPressingTrigger = true;
            }
        }

        if (!Input.GetKey(KeyCode.Mouse0))
        {
            if (currentWeapon != null)
            {
                currentWeapon.isPressingTrigger = false;
            }
        }
    }

    void RotateWeapon()
    {
        if (currentWeapon != null)
        {
            Quaternion rotation = CalculateRotationToLookAtMouse(currentWeapon.transform);
            currentWeapon.transform.rotation = rotation;

            if (rotation.eulerAngles.z > 90 && rotation.eulerAngles.z <= 270)
            {
                currentWeapon.transform.Find("Sprite").GetComponent<SpriteRenderer>().flipY = true;
            }
            else if (rotation.eulerAngles.z <= 90 || rotation.eulerAngles.z > 270)
            {
                currentWeapon.transform.Find("Sprite").GetComponent<SpriteRenderer>().flipY = false;
            }
        }
    }

    void FixedUpdate()
    {
        rb.velocity = moveDirection * character.moveSpeed;
        if (!disableMovements)
        {
            RotateWeapon();

        }
    }

    private Quaternion CalculateRotationToLookAtMouse(Transform rotatatingObjTransform)
    {
        Vector3 dir;
        float angle;
        dir = Input.mousePosition - Camera.main.WorldToScreenPoint(rotatatingObjTransform.transform.position);
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        return Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
