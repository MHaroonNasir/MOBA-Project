using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadingHealthBar : MonoBehaviour
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
        healthBarIndicatorManager.IncrementHealthBar(maxHealth);
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

    private void IncreaseHealth(float value) {
        currentHealthBarSlider.value += value;
        fadingHealthBarSlider.value += value;
    }

    private IEnumerator DecreaseHealth(float value) {
        currentHealthBarSlider.value -= value;

        for (int i = 0; i < 100; i++) {
            fadingHealthBarSlider.value -= value / 100f;
            yield return new WaitForSeconds(0.003f);
        }
    }
}
