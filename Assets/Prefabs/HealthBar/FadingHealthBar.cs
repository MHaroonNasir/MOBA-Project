using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadingHealthBar : CharacterTemplate
{
    public Slider currentHealthBarSlider;
    public Slider fadingHealthBarSlider;
    public HealthBarIndicatorManager healthBarIndicatorManager;

    void Start()
    {
        UpdateMaxHealth();
        IncreaseHealth();
    }

    /*private void Update() {
        if (Input.GetKeyDown(KeyCode.K))
        {
            IncreaseHealth(100);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            StartCoroutine(DecreaseHealth(100));
        }
    }*/

    public void UpdateHealthBar() {
        if (currentHealthBarSlider.maxValue != characterInfo.genericStatsAndActions.baseHealth) {
            UpdateMaxHealth();
        }

        if (currentHealthBarSlider.value < characterInfo.genericStatsAndActions.appliedHealth) {
            IncreaseHealth();
        }
        else if (currentHealthBarSlider.value > characterInfo.genericStatsAndActions.appliedHealth) {
            StartCoroutine(DecreaseHealth());
        }
    }

    //TODO: make a way to reduce maxhealth and indicators
    public void UpdateMaxHealth() {
        currentHealthBarSlider.maxValue = (float)characterInfo.genericStatsAndActions.baseHealth;
        fadingHealthBarSlider.maxValue = (float)characterInfo.genericStatsAndActions.baseHealth;
        healthBarIndicatorManager.IncrementHealthBar(characterInfo.genericStatsAndActions.baseHealth);
    }

    private void IncreaseHealth() {
        currentHealthBarSlider.value = (float)characterInfo.genericStatsAndActions.appliedHealth;
        fadingHealthBarSlider.value = (float)characterInfo.genericStatsAndActions.appliedHealth;
    }

    private IEnumerator DecreaseHealth() {
        double healthLossDifference = currentHealthBarSlider.value - characterInfo.genericStatsAndActions.appliedHealth;
        currentHealthBarSlider.value = (float)characterInfo.genericStatsAndActions.appliedHealth;

        for (int i = 0; i < 100; i++) {
            fadingHealthBarSlider.value -= (float)(healthLossDifference / 100d);
            yield return new WaitForSeconds(0.003f);
        }
    }

    private void OnEnable() {
        characterInfo.genericStatsAndActions.updateHealth += UpdateHealthBar;
    }

    private void OnDisable() {
        characterInfo.genericStatsAndActions.updateHealth -= UpdateHealthBar;
    }
}
