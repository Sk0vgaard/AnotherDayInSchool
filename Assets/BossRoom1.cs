﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossRoom1 : Room
{
    PlacyerController player;
    public BlockDoorObject blockDoor;
    private bool runOnce = true;
    [SerializeField] private string nextLevel;

    public float levelStartDelay = 0.1f;
    private Text levelText;
    private GameObject levelImage;
    private string currentLevel;

    public void Awake()
    {
        currentLevel = "Level 1";
        TextLevel();
    }

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
        SceneManager.LoadScene(nextLevel);
    }

    public void GameOver()
    {
        levelText.text = "Game Over \n Better luck in " + currentLevel + " next time...";
        levelImage.SetActive(true);
    }

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
