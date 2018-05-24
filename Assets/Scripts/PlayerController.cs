using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : HealthSystem {

    public Character character;
    public Animator animator;

    public bool disableMovements;

    private Vector2 moveDirection;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Weapon currentWeapon;
    private bool flipped;

    /// <summary>
    /// Gets references to the components for the player.
    /// </summary>
    new void Awake()
    {
        base.Awake();        
        character.Initialize();
        rb = GetComponent<Rigidbody2D>();
        sr = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        animator = transform.Find("Sprite").GetComponent<Animator>();
        animator.runtimeAnimatorController = character.animatorController;
    }

    /// <summary>
    /// Runs when the player has been instantiated.
    /// </summary>
    new void Start () {
        base.Start();
        GameObject weaponGameObject = Instantiate(character.startingWeapon.gameObject, transform.position, transform.rotation) as GameObject;
        weaponGameObject.transform.SetParent(this.transform);
        Weapon weapon = weaponGameObject.GetComponent<Weapon>();
        weapon.holder = GetComponent<HealthSystem>();
        currentWeapon = weapon;
        
    }
	
	void Update ()
	{
	    PlayerAnimator();

	}

    void FixedUpdate()
    {
        HandlePlayerActions();

        rb.velocity = moveDirection * character.moveSpeed;
    }

    /// <summary>
    /// When to move or not to move.
    /// </summary>
    private void HandlePlayerActions()
    {
        // if alive and able to move, move and shoot.
        if (!disableMovements && !isDead)
        {
            Movement();
            HandleShooting();
            RotateWeapon();
        }
        // If dead or unable to move, stop movement and shooting.
        else
        {
            moveDirection = new Vector2(0, 0);
            if (currentWeapon != null)
            {
                currentWeapon.isPressingTrigger = false;
            }
        }
    }

    /// <summary>
    /// Two diffents animations, one to walk and another when standing still.
    /// </summary>
    private void PlayerAnimator()
    {
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
    }

    void Movement()
    {
        moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }

    /// <summary>
    /// When to shoot.
    /// </summary>
    void HandleShooting()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (currentWeapon != null)
            {
                currentWeapon.isPressingTrigger = true;
            }
        }
        else
        {
            if (currentWeapon != null)
            {
            currentWeapon.isPressingTrigger = false;
            }
        }
    }

    /// <summary>
    /// Rotates the gun around the player.
    /// </summary>
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


    /// <summary>
    /// Used to make the gun rotate to follow the mouse.
    /// </summary>
    /// <param name="rotatatingObjTransform"></param>
    /// <returns></returns>
    private Quaternion CalculateRotationToLookAtMouse(Transform rotatatingObjTransform)
    {
        Vector3 dir;
        float angle;
        dir = Input.mousePosition - Camera.main.WorldToScreenPoint(rotatatingObjTransform.transform.position);
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        return Quaternion.AngleAxis(angle, Vector3.forward);
    }

    /// <summary>
    /// Run animation when player dies.
    /// </summary>
    public override void Die()
    {
        isDead = true;
        GameObject bloodSplatter = Resources.Load("blood") as GameObject;
        Instantiate(bloodSplatter, transform.position, transform.rotation);
        transform.Find("Sprite").gameObject.SetActive(false);
    }
}
