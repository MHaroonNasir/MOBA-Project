using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowHitbox : MonoBehaviour
{
    [Tooltip("True = slow lasts indefinately until the zone dissapears or the player leaves it. False = slow lasts for a set duration.")]
    public bool isPersistentSlow;
    [Tooltip("Value must be either 'f' for a flat speed decrease or '%' for % speed decrease.")]
    public char slowType;
    [Tooltip("Value in seconds. This value is ignored if isPersistentSlow enabled.")]
    public float duration;
    [Tooltip("If slowType is 'f', value can be any digit. If slowType is '%', value must be below 1.")]
    public float slowIntensity;

    Dictionary<string, CCSlow> chrIDs = new Dictionary<string, CCSlow>();

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            other.gameObject.TryGetComponent<Stats>(out Stats stats);
            other.gameObject.TryGetComponent<CCSlow>(out CCSlow ccSlow);
            if (!chrIDs.ContainsKey(stats.ID.playerName)) {
                chrIDs.Add(stats.ID.playerName, ccSlow);
                ApplySlowToEnemy(stats.ID.playerName);
                if (!isPersistentSlow) {
                    chrIDs.Remove(stats.ID.playerName);
                }
            }
        }
    }

    void ApplySlowToEnemy(string playerName) {
        if (isPersistentSlow) {
            chrIDs[playerName].ReceiveCCSlow(slowType, slowIntensity);
        } else {
            chrIDs[playerName].ReceiveCCSlow(duration, slowType, slowIntensity);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (isPersistentSlow) {
            other.gameObject.TryGetComponent<Stats>(out Stats stats);
            chrIDs[stats.ID.playerName].ReceiveCCSlow(slowType, 0f);
            chrIDs.Remove(stats.ID.playerName);
        }
    }

    private void OnDisable() {
        if (isPersistentSlow) {
            foreach (KeyValuePair<string, CCSlow> entry in chrIDs) {
                entry.Value.ReceiveCCSlow(slowType, 0f);
            }
            chrIDs.Clear();
        }
    }
}
