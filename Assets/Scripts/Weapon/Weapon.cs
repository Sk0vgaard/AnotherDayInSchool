using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {

    [HideInInspector]
    public HealthSystem holder;
    [HideInInspector]
    public bool isPressingTrigger;

    public void Awake()
    {
        
    }

}
