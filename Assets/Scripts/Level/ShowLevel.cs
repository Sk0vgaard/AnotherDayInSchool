﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowLevel : MonoBehaviour {

    public float levelStartDelay = 0.1f;
    private Text levelText;
    private GameObject levelImage;
    private AudioSource audioSource;
    public AudioClip music;
    public string currentLevel;


    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Awake()
    {
        audioSource = GameObject.Find("BGM").GetComponent<AudioSource>();
       
        TextLevel();
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

        audioSource.clip = music;
        audioSource.Play();

    }

    private void HideLevelImage()
    {
        levelImage.SetActive(false);
    }
}
