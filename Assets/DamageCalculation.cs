using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCalculation : MonoBehaviour
{
    [SerializeField] float health = 100f;
    public HealthUI healthUI;

    public float CalculateDamage(float damageValue)
    {
        return damageValue;
    }

    public void ReceiveDamage(float damage)
    {
        health -= damage;
        healthUI.Update3DSlider(health);
        if (health <= 0f)
        {
            TriggerDeath();
        }
    }

    private void TriggerDeath()
    {
        Debug.Log("DIED");
    }
}
