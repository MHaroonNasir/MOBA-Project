using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadingHealthBar : MonoBehaviour
{
    public float currentHealth, maxHealth, incrementHealth;
    public Slider currentHealthBarSlider;
    public Slider fadingHealthBarSlider;
    public HealthBarIndicatorManager healthBarIndicatorManager;

    public List<GameObject> indicators; //these 2 lists should be same size
    public List<Image> indicatorImages;

    void Start()
    {
        SetHealthBarSliderValues();
        UpdateIndicators();
        healthBarIndicatorManager.IncrementHealthBar(maxHealth);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(UpdateHealthBarSlider(100));
            Debug.Log("increase");
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            StartCoroutine(UpdateHealthBarSlider(-100));
            Debug.Log("decrease");
        }
    }

    public void SetHealthBarSliderValues() {
        currentHealthBarSlider.maxValue = maxHealth;
        currentHealthBarSlider.value = currentHealth;
        fadingHealthBarSlider.maxValue = maxHealth;
        fadingHealthBarSlider.value = currentHealth;
    }

    public void UpdateCurrentHealth(float currentHealth) {
        this.currentHealth = Mathf.Min(currentHealth, this.maxHealth); //choose lowest value, ensures currentHealth does not exceed maxHealth
    }

    public void UpdateMaxHealth(float maxHealth) {
        this.maxHealth = Mathf.Min(maxHealth, 3000f);
    }

    private IEnumerator UpdateHealthBarSlider(float value) {
        for (int i = 0; i < 100; i++) {
            currentHealthBarSlider.value += value;
            fadingHealthBarSlider.value += value / 500f;
            yield return new WaitForSeconds(0.003f);
        }
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
