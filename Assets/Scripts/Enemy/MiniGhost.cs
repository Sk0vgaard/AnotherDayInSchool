﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGhost : Enemy {

    private Transform destination;

    

    // Use this for initialization
    new void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void Activate(PlayerController player)
    {
        this.player = player;
    }

    public override void Deactivate(PlayerController player)
    {
        this.player = null;
    }
}
