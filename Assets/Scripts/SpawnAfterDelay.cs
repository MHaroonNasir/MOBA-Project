using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAfterDelay : MonoBehaviour
{
    public GameObject aftershockHitbox;

    public static int spawnDelayFrames = 25;
    public static int spawnDelayTime = spawnDelayFrames * 1000 / 60;
    public static int activeFrames = 125;
    public static int fullAnimationTime = (spawnDelayFrames + activeFrames) * 1000 / 60;

    IEnumerator SpawnDelay(Vector3 vector3Position, Vector3 vector3Rotation, int abilityStacks) {
        float timePassed = 0f;
        GameObject childAftershockHitbox = aftershockHitbox.transform.GetChild(abilityStacks).gameObject;
        
        while (timePassed <= spawnDelayTime) {
            timePassed += Time.deltaTime * 1000;
            yield return null;
        }
        childAftershockHitbox.transform.position = vector3Position;
        childAftershockHitbox.transform.eulerAngles = vector3Rotation;
        childAftershockHitbox.SetActive(true);
        //Debug.Log("aftershock shown");

        while (timePassed <= fullAnimationTime) {
            timePassed += Time.deltaTime * 1000;
            yield return null;
        }
        childAftershockHitbox.SetActive(false);
        //Debug.Log("aftershock hiddeen");
        childAftershockHitbox.transform.localPosition = new Vector3(0, this.gameObject.transform.localPosition.y, 0);
    }

    public void BeginSpawnDelay(Vector3 vector3Position, Vector3 vector3Rotation, int abilityStacks) {
        //Debug.Log("aftershock started");
        StartCoroutine(SpawnDelay(vector3Position, vector3Rotation, abilityStacks));
    }
}
