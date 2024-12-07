using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarIndicatorManager : MonoBehaviour
{
    public GameObject indicatorPrefab;
    int currentIndicatorChildren;

    public List<GameObject> indicators; //these 2 lists should be same size
    public List<Image> indicatorImages;

    public void IncrementHealthBar(float maxHealth) {
        currentIndicatorChildren = this.transform.childCount;
        float maxHealthIncrements = Mathf.Floor(maxHealth / 100f);
        
        while (currentIndicatorChildren < maxHealthIncrements) {
            GameObject childObject = Instantiate(indicatorPrefab, this.gameObject.transform);
            currentIndicatorChildren = this.transform.childCount;
            childObject.name = currentIndicatorChildren + "00HP Indicator";

            indicators.Add(childObject);
            indicatorImages.Add(childObject.GetComponent<Image>());
        }

        UpdateIndicators();
    }

    private void UpdateIndicators() {
        //casting to int
        int numOfIndicators = currentIndicatorChildren; //mathf.floor to ensure 799 / 100 leads to 7 indicator images, not rounded up 8

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
