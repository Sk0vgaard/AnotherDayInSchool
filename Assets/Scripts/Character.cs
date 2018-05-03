using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Characters/PlayableCharacter")]
public class Character : ScriptableObject {

    public float moveSpeed;
    public RuntimeAnimatorController animatorController;

    public void Initialize()
    {
        Debug.Log("Init Character");
        //this.animatorController = animatorController;
    }
}
