using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public float currentHealth, maxHealth, incrementHealth;

    void Start()
    {
        
    }

    public void UpdateCurrentHealth(float currentHealth) {
        this.currentHealth = Mathf.Min(currentHealth, this.maxHealth); //choose lowest value, ensures currentHealth does not exceed maxHealth
    }

    public void UpdateMaxHealth(float maxHealth) {
        this.maxHealth = maxHealth;
    }
}
