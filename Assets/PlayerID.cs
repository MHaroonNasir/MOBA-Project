using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu]
public class PlayerID : ScriptableObject
{
    public string playerName;
    public float movementSpeed = 3.5f;
    public float attackSpeed = 1f;
    public float attackRange = 3f;

    public float health = 100f;
    public float attackDamage = 10f;

    //public Action testAction;
    public PlayerEvents events; //struct //allocated on stack, not heap (research this)
    public Action sweepAttack;
    public Action ability1;

    public Action holdEnemyTargetting;
    public Action returnEnemyTargetting;
    
    public Action resetEnemyTargeting;
    public Action<float, string> dealDamage;

    //components
    //public PlayerMove playerMove;
}
