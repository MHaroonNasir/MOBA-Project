using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowHitbox : MonoBehaviour
{
    Dictionary<string, CCSlow> chrIDs = new Dictionary<string, CCSlow>();

    private void OnTriggerEnter(Collision other) {
        other.gameObject.TryGetComponent<PlayerID>(out PlayerID playerID);
        other.gameObject.TryGetComponent<CCSlow>(out CCSlow ccSlow);
        //int numberIDInChrSlows = chrSlows.Count;
        chrIDs.Add(playerID.playerName, ccSlow);
        //chrSlows.Add(ccSlow);
        StartCoroutine(ApplyContinuousSlow(playerID.playerName));
    }

    IEnumerator ApplyContinuousSlow(string playerName) {
        while (chrIDs.ContainsKey(playerName) == true) {
            chrIDs[playerName].ReceiveCCSlow(0.2f, '%', 0.65f);
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void OnTriggerExit(Collider other) {
        other.gameObject.TryGetComponent<PlayerID>(out PlayerID playerID);
        chrIDs.Remove(playerID.playerName);
    }

    private void OnDisable() {
        chrIDs.Clear();
    }
}
