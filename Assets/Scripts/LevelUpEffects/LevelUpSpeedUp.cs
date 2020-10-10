using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpSpeedUp : LevelUpEffectParent
{
    [SerializeField]
    float speedUp;

    public override void OnActivate()
    {
        FindObjectOfType<MainCharacterBehaviour>().buffSpeed(speedUp);
    }
}
