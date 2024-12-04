using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarIndicatorManager : MonoBehaviour
{
    public GameObject indicatorPrefab;

    public void IncrementHealthBar(float maxHealth) {
        int currentIndicatorChildren = this.transform.childCount;
        float maxHealthIncrements = Mathf.Floor(maxHealth / 100f);
        Debug.Log(maxHealthIncrements);
        
        while (currentIndicatorChildren < maxHealthIncrements) {
            GameObject childObject = Instantiate(indicatorPrefab, this.gameObject.transform);
            currentIndicatorChildren = this.transform.childCount;
            childObject.name = currentIndicatorChildren + "00HP Indicator";
        }
    }
}
