using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom2 : ARoom {

    PauseMenu menu;
    private bool alreadyCalled;

    public override void Enter(PlayerController player)
    {
        ActivateEnemies(player);
        isPlayerInRoom = true;
		GetComponent<AudioSource> ().Play ();
    }

    public override void Exit()
    {
        DeactivateEnemies();
        isPlayerInRoom = false;
    }

    // Use this for initialization
    new void Start () {
        base.Start();
        menu = FindObjectOfType<PauseMenu>();
	}
	
	// Update is called once per frame
	void Update () {

        if (roomClearOfEnemies && alreadyCalled == false)
        {
            alreadyCalled = true;
            menu.WinScreen();
        }
	}
}
