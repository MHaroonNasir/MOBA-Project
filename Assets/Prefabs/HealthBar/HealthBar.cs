using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public float currentHealth, maxHealth, incrementHealth;
    public Slider healthBarSlider;
    public HealthBarIndicatorManager healthBarIndicatorManager;

    public List<GameObject> indicators; //these 2 lists should be same size
    public List<Image> indicatorImages;

    void Start()
    {
        UpdateHealthBarSlider();
        UpdateIndicators();
        healthBarIndicatorManager.IncrementHealthBar(maxHealth);
    }

    public void UpdateCurrentHealth(float currentHealth) {
        this.currentHealth = Mathf.Min(currentHealth, this.maxHealth); //choose lowest value, ensures currentHealth does not exceed maxHealth
    }

    public void UpdateMaxHealth(float maxHealth) {
        this.maxHealth = Mathf.Min(maxHealth, 3000f);
    }

    private void UpdateHealthBarSlider() {
        healthBarSlider.maxValue = maxHealth;
        healthBarSlider.value = currentHealth;
    }

    private void UpdateIndicators() {
        //casting to int
        int numOfIndicators = (int)Mathf.Floor(maxHealth / currentHealth); //mathf.floor to ensure 799 / 100 leads to 7 indicator images, not rounded up 8

        for (int i = 0; i < indicators.Count; i++) {
            if (i < numOfIndicators) {
                indicators[i].SetActive(true);
                if (i == numOfIndicators - 1) {
                    indicatorImages[i].enabled = false;
                } else {
                    indicatorImages[i].enabled = true;
                }
            } else {
                indicators[i].SetActive(false);
            }
        }
    }
}
