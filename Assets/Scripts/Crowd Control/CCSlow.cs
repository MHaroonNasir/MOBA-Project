using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCSlow : CombatSystem
{
    public void ReceiveCCSlow(float duration, char slowType, float intensity) {
        StartCoroutine(ApplyCCSlow(duration, slowType, intensity));
    }

    public void ReceiveCCSlow(char slowType, float intensity) {
        ApplyCCSlow(slowType, intensity);
    }

    IEnumerator ApplyCCSlow(float duration, char slowType, float intensity) {
        float timePassed = 0f;

        if (slowType == 'f') {
            stats.ID.movementSpeedApplied = stats.ID.movementSpeedBase - intensity;
        }
        if (slowType == '%') {
            stats.ID.movementSpeedApplied = stats.ID.movementSpeedBase * (1 - intensity);
        }
        stats.ID.isSlowed?.Invoke();

        while (timePassed < duration) {
                timePassed += Time.deltaTime;
                yield return null; //return after each frame
            }
        stats.ID.movementSpeedApplied = stats.ID.movementSpeedBase;
        stats.ID.isSlowed?.Invoke();
    }

    void ApplyCCSlow(char slowType, float intensity) {
        if (slowType == 'f') {
            stats.ID.movementSpeedApplied = stats.ID.movementSpeedBase - intensity;
        }

        if (slowType == '%') {
            stats.ID.movementSpeedApplied = stats.ID.movementSpeedBase * (1 - intensity);
        }
        stats.ID.isSlowed?.Invoke();
    }
}
