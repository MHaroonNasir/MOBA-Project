using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiPassive : CharacterTemplate
{
    public float currentHealthBeforeUpdate, currentHealthAfterUpdate, baseMovementSpeed, appliedMovementSpeed;

    public bool isNotInCombat = true;
    public float passiveMovementSpeedIncrease = 1f;
    public float passiveMovementSpeedDecayDuration = 2.5f;
    public float passiveRechargeDuration = 12f;

    private void Start() {
        currentHealthBeforeUpdate = characterInfo.genericStatsAndActions.appliedHealth;
        ReapplyPassive();
    }

    public void ReapplyPassive() {
        characterInfo.genericStatsAndActions.baseMovementSpeed += passiveMovementSpeedIncrease;
        characterInfo.genericStatsAndActions.appliedMovementSpeed += passiveMovementSpeedIncrease;
        characterInfo.genericStatsAndActions?.updateMovementSpeed.Invoke();
    }

    public void CheckPlayerHasTakenDamage() {
        currentHealthAfterUpdate = characterInfo.genericStatsAndActions.appliedHealth;
        if (currentHealthAfterUpdate < currentHealthBeforeUpdate) {
            StartCoroutine(PassiveMovementSpeedDecay());
            StopCoroutine(PassiveRecharge());
            StartCoroutine(PassiveRecharge());
        }
        currentHealthBeforeUpdate = currentHealthAfterUpdate;
    }

    IEnumerator PassiveMovementSpeedDecay() {
        float timePassed = 0f;

        while (timePassed < passiveMovementSpeedDecayDuration) {
            baseMovementSpeed = characterInfo.genericStatsAndActions.baseMovementSpeed;
            appliedMovementSpeed = characterInfo.genericStatsAndActions.appliedMovementSpeed;

            characterInfo.genericStatsAndActions.baseMovementSpeed = Mathf.Lerp(baseMovementSpeed, baseMovementSpeed-passiveMovementSpeedIncrease, passiveMovementSpeedDecayDuration);
            characterInfo.genericStatsAndActions.appliedMovementSpeed = Mathf.Lerp(appliedMovementSpeed, appliedMovementSpeed-passiveMovementSpeedIncrease, passiveMovementSpeedDecayDuration);
            timePassed += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator PassiveRecharge() {
        float timePassed = 0f;

        while (timePassed < passiveRechargeDuration) {
            timePassed += Time.deltaTime;
            yield return null;
        }
        ReapplyPassive();
    }

    private void OnEnable() {
        characterInfo.genericStatsAndActions.updateMovementSpeed += CheckPlayerHasTakenDamage;
    }

    private void OnDisable() {
        characterInfo.genericStatsAndActions.updateMovementSpeed -= CheckPlayerHasTakenDamage;
    }
}
