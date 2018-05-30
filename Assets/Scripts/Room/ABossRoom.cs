using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class ABossRoom : ARoom {
    public AudioClip bossMusic;

    // Use this for initialization
    public new void Start () {
	    base.Start();	
	}

    public override void Enter(PlayerController player)
    {
        bgm.clip = bossMusic;
        bgm.Play();
    }

    public override void Exit()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
