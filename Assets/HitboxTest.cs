using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxTest : MonoBehaviour
{
    EnemyInteraction enemyInteraction;
    DamageCalculation enemyDamageCalculation;

    private void Start() {
        //enemyInteraction = GetComponent<EnemyInteraction>();
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Enemy") {
            enemyDamageCalculation = other.gameObject.GetComponent<DamageCalculation>();
            enemyDamageCalculation.ReceiveDamage(15f);
        }
    }
}
