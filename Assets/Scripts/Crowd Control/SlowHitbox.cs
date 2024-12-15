using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowHitbox : MonoBehaviour
{
    //Dictionary<string, CCSlow> chrIDs = new Dictionary<string, CCSlow>();
    List<string> chrNames = new List<string>();
    List<Stats> chrStats = new List<Stats>();
    List<CCSlow> chrSlows = new List<CCSlow>();

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            Debug.Log("ontriggerenter");
            other.gameObject.TryGetComponent<Stats>(out Stats stats);
            other.gameObject.TryGetComponent<CCSlow>(out CCSlow ccSlow);
            //chrIDs.Add(stats.ID.playerName, ccSlow);
            if (chrNames.Contains(stats.ID.playerName) == false) {
                chrNames.Add(stats.ID.playerName);
                chrStats.Add(stats);
                chrSlows.Add(ccSlow);
            }
            StartCoroutine(ApplyContinuousSlow(stats.ID.playerName));
        }
    }

    IEnumerator ApplyContinuousSlow(string playerName) {
        /*while (chrIDs.ContainsKey(playerName) == true) {
            chrIDs[playerName].ReceiveCCSlow(0.2f, '%', 0.65f);
            
            yield return new WaitForSeconds(0.2f);
        }*/
        while (chrNames.Contains(playerName) == true) {
            int index = chrNames.IndexOf(playerName);
            //chrStats[index].ID.isSlowed?.Invoke();
            chrSlows[index].ReceiveCCSlow(0.2f, '%', 0.65f);
            
            yield return new WaitForSeconds(0.2f);
        }

    }

    private void OnTriggerExit(Collider other) {
        other.gameObject.TryGetComponent<Stats>(out Stats stats);
        //chrIDs.Remove(stats.ID.playerName);
        int index = chrNames.IndexOf(stats.ID.playerName);
        chrNames.RemoveAt(index);
        chrStats.RemoveAt(index);
        chrSlows.RemoveAt(index);
    }

    private void OnDisable() {
        //chrIDs.Clear();
        chrNames.Clear();
        chrStats.Clear();
        chrSlows.Clear();
    }
}
