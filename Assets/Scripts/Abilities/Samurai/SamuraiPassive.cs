using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiPassive : CharacterTemplate
{
    public bool isNotInCombat = true;
    public float passiveMovementSpeedIncrease = 1f;

    private void Start() {
        characterInfo.genericStatsAndActions.baseMovementSpeed += passiveMovementSpeedIncrease;
        characterInfo.genericStatsAndActions?.updateMovementSpeed.Invoke();
    }
}
