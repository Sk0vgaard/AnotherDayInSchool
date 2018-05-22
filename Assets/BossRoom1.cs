using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossRoom1 : Room
{
    PlacyerController player;
    public BlockDoorObject blockDoor;
    private bool runOnce = true;
    [SerializeField] private string newLevel;


    public void Update()
    {
        if (roomClearOfEnemies)
        {
            runOnce = false;
            // Fadeout
            StartCoroutine(ChangeLevel());
        }
    }

    IEnumerator ChangeLevel()
    {
        //Waits for the animation to be done.
        yield return new WaitForSeconds(1.8f);

        float fadeTime = GameObject.Find("Room3 (BossRoom)").GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime + 1.8f);
        SceneManager.LoadScene(newLevel);
    }

    public override void Enter(PlacyerController player)
    {
        this.player = player;
        if (!blockDoor.IsOpen())
        {
            blockDoor.Open();

        }
    }

    public override void Exit()
    {
        this.player = null;
    }

    public void StartFight(PlacyerController player)
    {
        ActivateEnemies(player);
        if (blockDoor.IsOpen())
        {
            blockDoor.Close();
        }
    }

    


}
