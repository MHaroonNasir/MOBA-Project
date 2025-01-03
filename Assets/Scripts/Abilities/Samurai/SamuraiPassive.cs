using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiPassive : CharacterTemplate
{
    public float currentHealthBeforeUpdate, currentHealthAfterUpdate, movementSpeedBase, movementSpeedApplied;

    public bool inCombat = false;
    public float passiveMovementSpeedIncrease = 1f;
    public float passiveMovementSpeedDecayDuration = 2.5f;
    public float passiveRechargeDuration = 12f;

    private void Start() {
        currentHealthBeforeUpdate = characterInfo.genericStatsAndActions.appliedHealth;
        ReapplyPassive();
    }

    public void ReapplyPassive() {
        Debug.Log("reapply passive");
        characterInfo.genericStatsAndActions.movementSpeedFlatModification += passiveMovementSpeedIncrease;
        inCombat = false;
        characterInfo.genericStatsAndActions?.updateMovementSpeed.Invoke();
    }

    public void CheckPlayerHasTakenDamage() {
        Debug.Log("check damage");
        currentHealthAfterUpdate = characterInfo.genericStatsAndActions.appliedHealth;
        if (currentHealthAfterUpdate < currentHealthBeforeUpdate) {
            if (!inCombat) {
                StartCoroutine(PassiveMovementSpeedDecay());
            }
            StopCoroutine(PassiveRecharge());
            StartCoroutine(PassiveRecharge());
        }
        currentHealthBeforeUpdate = currentHealthAfterUpdate;
    }

    IEnumerator PassiveMovementSpeedDecay() {
        Debug.Log("passive decay");
        float timePassed = 0f;
        inCombat = true;
        characterInfo.genericStatsAndActions.movementSpeedFlatModification -= passiveMovementSpeedIncrease;

        movementSpeedBase = characterInfo.genericStatsAndActions.movementSpeedBase + passiveMovementSpeedIncrease;
        movementSpeedApplied = movementSpeedBase - passiveMovementSpeedIncrease;

        while (timePassed < passiveMovementSpeedDecayDuration) {
            characterInfo.genericStatsAndActions.movementSpeedBase = Mathf.Lerp(movementSpeedBase, movementSpeedApplied, timePassed);
            characterInfo.genericStatsAndActions.movementSpeedApplied = Mathf.Lerp(movementSpeedApplied, movementSpeedApplied, timePassed);
            characterInfo.genericStatsAndActions?.updateMovementSpeed.Invoke();
            timePassed += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator PassiveRecharge() {
        Debug.Log("passive recharge");
        float timePassed = 0f;

        while (timePassed < passiveRechargeDuration) {
            timePassed += Time.deltaTime;
            yield return null;
        }
        ReapplyPassive();
    }

    private void OnEnable() {
        characterInfo.genericStatsAndActions.updateHealth += CheckPlayerHasTakenDamage;
    }

    private void OnDisable() {
        characterInfo.genericStatsAndActions.updateHealth -= CheckPlayerHasTakenDamage;
    }
}
