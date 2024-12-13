using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowHitbox : MonoBehaviour
{
    Dictionary<string, CCSlow> chrIDs = new Dictionary<string, CCSlow>();

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            Debug.Log("ontriggerenter");
            other.gameObject.TryGetComponent<Stats>(out Stats stats);
            other.gameObject.TryGetComponent<CCSlow>(out CCSlow ccSlow);
            //int numberIDInChrSlows = chrSlows.Count;
            chrIDs.Add(stats.ID.playerName, ccSlow);
            //chrSlows.Add(ccSlow);
            StartCoroutine(ApplyContinuousSlow(stats.ID.playerName));
        }
    }

    IEnumerator ApplyContinuousSlow(string playerName) {
        while (chrIDs.ContainsKey(playerName) == true) {
            chrIDs[playerName].ReceiveCCSlow(0.2f, '%', 0.65f);
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void OnTriggerExit(Collider other) {
        other.gameObject.TryGetComponent<Stats>(out Stats stats);
        chrIDs.Remove(stats.ID.playerName);
    }

    private void OnDisable() {
        chrIDs.Clear();
    }
}
