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
            hasSwordHitEnemy = true;
        }
    }

    private void OnEnable() {
        //Debug.Log("hitbox enabled");
        hasSwordHitEnemy = false;
    }

    private void OnDisable() {
        //Debug.Log("hitbox disabled");
        if (hasSwordHitEnemy == true) {
            SwordHitEnemy?.Invoke();
        } else {
            SwordNotHitEnemy?.Invoke();
        }
    }
}
