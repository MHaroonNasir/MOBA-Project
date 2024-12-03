using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarIndicatorManager : MonoBehaviour
{
    public GameObject indicatorPrefab;

    public void IncrementHealthBar(float maxHealth) {
        int currentIndicatorChildren = this.transform.childCount;
        int maxHealthIncrements = (int)(maxHealth % 100);
        if (currentIndicatorChildren < maxHealthIncrements) {
            GameObject childObject = Instantiate(indicatorPrefab, this.gameObject.transform);
            childObject.name = "Increment " + maxHealthIncrements;
        }
    }
}
