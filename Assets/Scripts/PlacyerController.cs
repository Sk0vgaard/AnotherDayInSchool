using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacyerController : HealthSystem {

    public Character character;
    public Animator animator;

    private Vector2 moveDirection;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Weapon currentWeapon;
    private bool flipped;


    private void Awake()
    {
        character.Initialize();
        rb = GetComponent<Rigidbody2D>();
        sr = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        animator = transform.Find("Sprite").GetComponent<Animator>();
        animator.runtimeAnimatorController = character.animatorController;
    }

    void Start () {

        GameObject weaponGameObject = Instantiate(character.startingWeapon.gameObject, transform.position, transform.rotation) as GameObject;
        weaponGameObject.transform.SetParent(this.transform);
        currentWeapon = weaponGameObject.GetComponent<Weapon>();
        //animator.SetFloat("speed", 10.0f);
    }
	
	void Update () {
        moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
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

        if (currentWeapon != null)
        {
            Quaternion rotation = CalculateRotationToLookAtMouse(currentWeapon.transform);
            currentWeapon.transform.rotation = rotation;

            if (rotation.eulerAngles.z > 90 && rotation.eulerAngles.z <= 270)
            {
                currentWeapon.transform.Find("Sprite").GetComponent<SpriteRenderer>().flipY = true;
                //rotator.rotation = Quaternion.Euler(0, rotator.eulerAngles.y, rotator.eulerAngles.z); //.GetComponent<SpriteRenderer>().flipY = false;
                //currentWeapon.transform.Find("Rotator").transform.RotateAround(currentWeapon.transform.Find("Rotator").transform.position, transform.up, 180);
            }
            else if(rotation.eulerAngles.z <= 90 || rotation.eulerAngles.z > 270)
            {
                currentWeapon.transform.Find("Sprite").GetComponent<SpriteRenderer>().flipY = false;

                //rotator.rotation = Quaternion.Euler(180, rotator.eulerAngles.y, rotator.eulerAngles.z);
                //currentWeapon.transform.Find("Rotator").transform.RotateAround(currentWeapon.transform.Find("Rotator").transform.position, transform.up, 0);


            }

        }
    }

    void FixedUpdate()
    {
        rb.velocity = moveDirection * character.moveSpeed;
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
