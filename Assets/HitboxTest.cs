using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxTest : MonoBehaviour
{
    EnemyInteraction enemyInteraction;
    DamageCalculation enemyDamageCalculation;

    public Action SwordHitEnemy; 
    public Action SwordNotHitEnemy;
    bool hasSwordHitEnemy = false;

    private void Start() {
        //enemyInteraction = GetComponent<EnemyInteraction>();
        //Debug.Log("hitbox started");
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Enemy") {
            enemyDamageCalculation = other.gameObject.GetComponent<DamageCalculation>();
            enemyDamageCalculation.ReceiveDamage(15f);
            SwordHitEnemy?.Invoke();
            hasSwordHitEnemy = true; //crude way of ensuring if sword has hit, the OnDisable code logic does not run
        }
    }

    private void OnEnable() {
        //Debug.Log("hitbox enabled");
        hasSwordHitEnemy = false;
    }

    private void OnDisable() {
        //Debug.Log("hitbox disabled");
        if (hasSwordHitEnemy == false) {
            SwordNotHitEnemy?.Invoke();
        }
    }
}
