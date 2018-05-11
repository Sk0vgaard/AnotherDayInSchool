using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDoorObject : MonoBehaviour {

    private Animator anim;
    private BoxCollider2D collider;

    private bool open;
    //public bool open { get; }

    private void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    public bool IsOpen()
    {
        return open;
    }

    public void Open()
    {
        if (!open)
        {
            open = true;
            anim.SetBool("open", true);
            collider.enabled = false;
        }       
    }

    public void Close()
    {
        if (open)
        {
            open = false;
            anim.SetBool("open", false);
            collider.enabled = true;
        }
    }

}
