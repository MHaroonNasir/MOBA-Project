using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : CharacterTemplate
{
    public float maxHealth, currentHealth, maxPhysicalDef, currentPhysicalDef, maxMagicDef, currentMagicDef;

    void Start()
    {
        StartCoroutine(NaturalHealthRegen());
    }

    IEnumerator NaturalHealthRegen() {
        bool isCurrentlyAlive = characterInfo.genericStatsAndActions.isAlive;
        float regenAmount, currentHealthRegenerated;

        while (isCurrentlyAlive) {
            currentHealth = characterInfo.genericStatsAndActions.appliedHealth;
            maxHealth = characterInfo.genericStatsAndActions.baseHealth;
            regenAmount = characterInfo.genericStatsAndActions.appliedHealthRegen;
            currentHealthRegenerated = currentHealth + regenAmount;

            characterInfo.genericStatsAndActions.appliedHealth = Math.Min(currentHealthRegenerated, maxHealth);
            isCurrentlyAlive = characterInfo.genericStatsAndActions.isAlive;
            characterInfo.genericStatsAndActions?.updateHealth.Invoke();
            yield return new WaitForSeconds(1f);
        }
    }

    public void ReceiveDamage(float damage) {
        currentPhysicalDef = characterInfo.genericStatsAndActions.appliedPhysicalDefense;
        currentMagicDef = characterInfo.genericStatsAndActions.appliedMagicDefense;
        
        //todo: use DEF stats to reduce damage

        characterInfo.genericStatsAndActions.appliedHealth -= damage;
        if (characterInfo.genericStatsAndActions.appliedHealth <= 0f) {
            characterInfo.genericStatsAndActions?.hasDied.Invoke();
        }

    }

    public void IncreaseHealth() {

    }

    public void IncreaseMaxHealth() {

    }

}
