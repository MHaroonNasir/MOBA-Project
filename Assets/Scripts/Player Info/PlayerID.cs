using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu]
public class PlayerID : ScriptableObject
{
    public string playerName = "samurai";

    //health and sheilds
    public float baseHealth = 100f;
    public float appliedHealth = 100f;
    public float baseHealthRegen = 1.667f;
    public float appliedHealthRegen = 1.667f;
    public float shieldAmount = 0f;

    //mana
    public float baseMana = 100f;
    public float appliedMana = 100f;
    public float baseManaRegen = 5f;
    public float appliedManaRegen = 5f;

    //ressistances
    public float basePhysicalDefense = 0f;
    public float appliedPhysicalDefense = 0f;
    public float baseMagicDefense = 0f;
    public float appliedMagicDefense = 0f;

    //damage
    public float basePhysicalDamage = 10f;
    public float appliedPhysicalDamage = 10f;
    public float baseMagicDamage = 10f;
    public float appliedMagicDamage = 10f;

    //attack speed and range
    public float baseAttackSpeed = 1f;
    public float appliedAttackSpeed = 1f;
    public Action updateAttackSpeed;
    public float baseAttackRange = 3f;
    public float appliedAttackRange = 3f;

    //critical strike
    //public int critStrikeChance;

    //movement speed
    public float baseMovementSpeed = 3.5f;
    public float appliedMovementSpeed = 3.5f;
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
