using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowHitbox : MonoBehaviour
{
    Dictionary<string, CCSlow> chrIDs = new Dictionary<string, CCSlow>();
    //List<string> chrNames = new List<string>();
    //List<CCSlow> chrSlows = new List<CCSlow>();

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            other.gameObject.TryGetComponent<Stats>(out Stats stats);
            other.gameObject.TryGetComponent<CCSlow>(out CCSlow ccSlow);
            chrIDs.Add(stats.ID.playerName, ccSlow);
            ApplySlowToEnemy(stats.ID.playerName);
            /*if (chrNames.Contains(stats.ID.playerName) == false) {
                chrNames.Add(stats.ID.playerName);
                chrSlows.Add(ccSlow);
            }*/
            //StartCoroutine(ApplyContinuousSlow(stats.ID.playerName));
        }
    }

    void ApplySlowToEnemy(string playerName) {
        chrIDs[playerName].ReceiveCCSlow('%', 0.5f);
    }

    IEnumerator ApplyContinuousSlow(string playerName) {
        while (chrIDs.ContainsKey(playerName) == true) {
            chrIDs[playerName].ReceiveCCSlow(0.16f, '%', 0.65f);
            
            yield return new WaitForSeconds(0.15f);
        }
        /*while (chrNames.Contains(playerName) == true) {
            int index = chrNames.IndexOf(playerName);
            chrSlows[index].ReceiveCCSlow(0.16f, '%', 0.65f);
            Debug.Log("recieve cc slow");
            yield return new WaitForSeconds(0.15f);
        }*/

    }

    private void OnTriggerExit(Collider other) {
        other.gameObject.TryGetComponent<Stats>(out Stats stats);
        chrIDs[stats.ID.playerName].ReceiveCCSlow('%', 0f);
        chrIDs.Remove(stats.ID.playerName);
        //int index = chrNames.IndexOf(stats.ID.playerName);
        //chrNames.RemoveAt(index);
        //chrSlows.RemoveAt(index);
        //Debug.Log("removed from cc slow");
    }

    private void OnDisable() {
        foreach (KeyValuePair<string, CCSlow> entry in chrIDs) {
            entry.Value.ReceiveCCSlow('%', 0f);
        }
        chrIDs.Clear();
        //chrNames.Clear();
        //chrSlows.Clear();
    }
}
