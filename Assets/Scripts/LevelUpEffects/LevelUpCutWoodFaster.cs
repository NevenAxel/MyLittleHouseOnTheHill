using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpCutWoodFaster : LevelUpEffectParent
{
    [SerializeField]
    float speedFactor;

    public override void OnActivate()
    {
        FindObjectOfType<MainCharacterBehaviour>().buffCutTime(speedFactor);
    }
}
