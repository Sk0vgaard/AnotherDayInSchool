using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour {


    public List<Enemy> enemies;

    void Awake()
    {

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Exit()
    {
        foreach (var enemy in enemies)
        {
            enemy.player = null;
        }
    }

    public void Enter(PlacyerController player)
    {
        foreach (var enemy in enemies)
        {
            enemy.player = player;
        }
    }
}
