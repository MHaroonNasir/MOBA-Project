using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiPassive : CombatSystem
{
    public float passiveSpeedIncrease = 1f;

    private void Start() {
        stats.ID.baseMovementSpeed += passiveSpeedIncrease;
    }
}
