using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossRoom1 : ARoom
{
    PlayerController player;
    public BlockDoorObject blockDoor;
    private bool runOnce = true;
    [SerializeField] private string nextLevel;

    public float levelStartDelay = 0.1f;
    private Text levelText;
    private GameObject levelImage;
    private string currentLevel;

    public new void Awake()
    {
        base.Awake();
        currentLevel = "Level 1";
        TextLevel();
    }

    public void Update()
    {
        if (roomClearOfEnemies && runOnce)
        {
            runOnce = false;
            // Fadeout
            StartCoroutine(ChangeLevel());
        }
    }

    IEnumerator ChangeLevel()
    {
        //Waits for the death animation to be done.
        yield return new WaitForSeconds(1.8f);

        float fadeTime = GameObject.Find("Room3 (BossRoom)").GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime + 1.8f);
        SceneManager.LoadScene(nextLevel);
    }

    /// <summary>
    /// Sets the text level.
    /// </summary>
    private void TextLevel()
    {
        levelImage = GameObject.Find("LevelImage");
        levelText = GameObject.Find("LevelText").GetComponent<Text>();
        levelText.text = currentLevel;
        levelImage.SetActive(true);
        Invoke("HideLevelImage", levelStartDelay);
    }

    private void HideLevelImage()
    {
        levelImage.SetActive(false);
    }

    /// <summary>
    /// When player enters the room.
    /// </summary>
    /// <param name="player"></param>
    public override void Enter(PlayerController player)
    {
        this.player = player;
        // The player shouldnt be able to go out of the room.
        if (!blockDoor.IsOpen())
        {
            blockDoor.Open();
        }
        isPlayerInRoom = true;
    }

    public override void Exit()
    {
        this.player = null;
        isPlayerInRoom = false;
    
    }

    /// <summary>
    /// When the fight agianst the boss starts, lock the door.
    /// </summary>
    /// <param name="player"></param>
    public void StartFight(PlayerController player)
    {
        //Activate boss.
        ActivateEnemies(player);

        if (blockDoor.IsOpen())
        {
            blockDoor.Close();
        }
    }

    


}
