using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu]
public class GenericCharacterStatsAndActions : ScriptableObject
{
    public string playerName;
    public bool isAlive = true;
    public Action hasDied;

    //health and sheilds
    public double baseHealth = 100d;
    public double appliedHealth = 100d;
    public Action updateHealth;
    public double baseHealthRegen = 1.667d;
    public double appliedHealthRegen = 1.667d;
    public Action updateHealthRegen;
    public double shieldAmount = 0d;

    //mana
    public double baseMana = 100d;
    public double appliedMana = 100d;
    public double baseManaRegen = 5d;
    public double appliedManaRegen = 5d;

    //ressistances
    public double basePhysicalDefense = 0d;
    public double appliedPhysicalDefense = 0d;
    public double baseMagicDefense = 0d;
    public double appliedMagicDefense = 0d;

    //damage
    public double basePhysicalDamage = 10d;
    public double appliedPhysicalDamage = 10d;
    public double baseMagicDamage = 10d;
    public double appliedMagicDamage = 10d;

    //attack speed and range
    public double baseAttackSpeed = 1d;
    public double appliedAttackSpeed = 1d;
    public Action updateAttackSpeed;
    public double baseAttackRange = 3d;
    public double appliedAttackRange = 3d;
    public Action updateAttackRange;

    //critical strike
    //public int critStrikeChance;

    //movement speed
    public double movementSpeedBase = 3.5d;
    public double movementSpeedFlatModification = 0d;
    public double movementSpeedPercentModification = 1.0d;
    public double movementSpeedApplied = 3.5d;
    public Action updateMovementSpeed;

    //public Action testAction;
    public PlayerEvents events; //struct //allocated on stack, not heap (research this)
    public Action sweepAttack;
    public Action ability1;

    public Action holdEnemyTargetting;
    public Action returnEnemyTargetting;
    
    public Action resetEnemyTargeting;
    public Action<float, string> dealDamage;

    //CC scripts
    public Action isSlowed;
    public Action ccEnded;

    //components
    //public PlayerMove playerMove;
}
