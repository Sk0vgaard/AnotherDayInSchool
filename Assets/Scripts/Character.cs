using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Characters/PlayableCharacter")]
public class Character : ScriptableObject {

    public float moveSpeed;
    public RuntimeAnimatorController animatorController;
    public Weapon startingWeapon;

    public void Initialize()
    {

    }
}
