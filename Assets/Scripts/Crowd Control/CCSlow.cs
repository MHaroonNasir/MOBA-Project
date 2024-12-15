using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCSlow : CombatSystem
{
    public void ReceiveCCSlow(float duration, char slowType, float intensity) {
        StartCoroutine(ApplyCCSlow(duration, slowType, intensity));
    }

    private IEnumerator ApplyCCSlow(float duration, char slowType, float intensity) {
        float timePassed = 0f;

        while (timePassed < duration && slowType == 'f') {
            stats.ID.appliedMovementSpeed = stats.ID.baseMovementSpeed - intensity;
            timePassed += Time.deltaTime;
            stats.ID.isSlowed?.Invoke();
            yield return null;
        }

        while (timePassed < duration && slowType != 'f') {
            stats.ID.appliedMovementSpeed = stats.ID.baseMovementSpeed * (1 - intensity);
            timePassed += Time.deltaTime;
            stats.ID.isSlowed?.Invoke();
            yield return null;
        }

        stats.ID.appliedMovementSpeed = stats.ID.baseMovementSpeed;
        stats.ID.isSlowed?.Invoke();
    }
}
