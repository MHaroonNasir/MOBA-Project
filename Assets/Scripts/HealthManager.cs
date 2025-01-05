using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : CharacterTemplate
{
    public double maxHealth, currentHealth, maxPhysicalDef, currentPhysicalDef, maxMagicDef, currentMagicDef;

    void Start()
    {
        StartCoroutine(NaturalHealthRegen());
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.O)) {
            ReceiveDamage(20);
        }
        if (Input.GetKeyDown(KeyCode.P)) {
            IncreaseHealth(20);
        }
    }

    IEnumerator NaturalHealthRegen() {
        bool isCurrentlyAlive = characterInfo.genericStatsAndActions.isAlive;
        double regenAmount, currentHealthRegenerated;

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

    public void ReceiveDamage(double damage) {
        currentPhysicalDef = characterInfo.genericStatsAndActions.appliedPhysicalDefense;
        currentMagicDef = characterInfo.genericStatsAndActions.appliedMagicDefense;
        
        //todo: use DEF stats to reduce damage

        characterInfo.genericStatsAndActions.appliedHealth -= damage;
        if (characterInfo.genericStatsAndActions.appliedHealth <= 0d) {
            characterInfo.genericStatsAndActions?.hasDied.Invoke();
        }
        characterInfo.genericStatsAndActions?.updateHealth.Invoke();
    }

    public void IncreaseHealth(float health) {
        double currentHealthWithIncrease = characterInfo.genericStatsAndActions.appliedHealth + health;
        maxHealth = characterInfo.genericStatsAndActions.baseHealth;
        characterInfo.genericStatsAndActions.appliedHealth = Mathf.Min((float)currentHealthWithIncrease, (float)maxHealth);
        characterInfo.genericStatsAndActions?.updateHealth.Invoke();
    }

    public void IncreaseMaxHealth() {

    }

}
