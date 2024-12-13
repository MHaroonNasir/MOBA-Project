using System;
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

    RectTransform indicatorManager;

    private void Start() {
        indicatorManager = this.gameObject.GetComponent<RectTransform>();
    }

    public void IncrementHealthBar(float maxHealth) {
        currentIndicatorChildren = this.transform.childCount;
        float widthPerIncrement = 1000 / maxHealth * 100;
        float numOfIndicators = Mathf.Ceil(1000 / widthPerIncrement);
        float widthIncreaseForLastIncrement = widthPerIncrement - (1000 / widthPerIncrement % 1 * widthPerIncrement);
        //float maxHealthIncrements = Mathf.Floor(maxHealth / 100f);
        
        while (currentIndicatorChildren < numOfIndicators) {
            GameObject childObject = Instantiate(indicatorPrefab, this.gameObject.transform);
            currentIndicatorChildren = this.transform.childCount;
            childObject.name = currentIndicatorChildren + "00HP Indicator";

            indicators.Add(childObject);
            indicatorImages.Add(childObject.GetComponent<Image>());
        }

        //Debug.Log(currentIndicatorChildren);
        //Debug.Log(widthPerIncrement);
        //Debug.Log(numOfIndicators);
        //Debug.Log(widthIncreaseForLastIncrement);

        UpdateIndicators();
        UpdateIndicatorCanvas(widthIncreaseForLastIncrement);
    }

    private void UpdateIndicators() {
        for (int i = 0; i < indicators.Count; i++) {
            if (i < currentIndicatorChildren) {
                indicators[i].SetActive(true);
                if (i == currentIndicatorChildren - 1) {
                    indicatorImages[i].enabled = false;
                } else {
                    indicatorImages[i].enabled = true;
                }
            } else {
                indicators[i].SetActive(false);
            }
        }
    }

    void UpdateIndicatorCanvas(float widthIncreaseForLastIncrement) {
        indicatorManager.offsetMax = new Vector2(widthIncreaseForLastIncrement - 10, indicatorManager.offsetMax.y);
    }
}
