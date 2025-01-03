using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadingHealthBar : CharacterTemplate
{
    public float currentHealth, maxHealth;
    public Slider currentHealthBarSlider;
    public Slider fadingHealthBarSlider;
    public HealthBarIndicatorManager healthBarIndicatorManager;

    public List<GameObject> indicators; //these 2 lists should be same size
    public List<Image> indicatorImages;

    void Start()
    {
        SetHealthBarSliderValues();
        healthBarIndicatorManager.IncrementHealthBar(characterInfo.genericStatsAndActions.baseHealth);
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

    public void SetHealthBarSliderValues() {
        currentHealthBarSlider.maxValue = characterInfo.genericStatsAndActions.baseHealth;
        currentHealthBarSlider.value = characterInfo.genericStatsAndActions.appliedHealth;
        fadingHealthBarSlider.maxValue = characterInfo.genericStatsAndActions.baseHealth;
        fadingHealthBarSlider.value = characterInfo.genericStatsAndActions.appliedHealth;
    }

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

    public void UpdateMaxHealth() {
        currentHealthBarSlider.maxValue = characterInfo.genericStatsAndActions.baseHealth;
        fadingHealthBarSlider.maxValue = characterInfo.genericStatsAndActions.baseHealth;
        healthBarIndicatorManager.IncrementHealthBar(characterInfo.genericStatsAndActions.baseHealth);
    }

    private void IncreaseHealth() {
        currentHealthBarSlider.value = characterInfo.genericStatsAndActions.appliedHealth;
        fadingHealthBarSlider.value = characterInfo.genericStatsAndActions.appliedHealth;
    }

    private IEnumerator DecreaseHealth() {
        float healthLossDifference = currentHealthBarSlider.value - characterInfo.genericStatsAndActions.appliedHealth;
        currentHealthBarSlider.value = characterInfo.genericStatsAndActions.appliedHealth;

        for (int i = 0; i < 100; i++) {
            fadingHealthBarSlider.value -= healthLossDifference / 100f;
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
