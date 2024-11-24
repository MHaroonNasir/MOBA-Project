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

    IEnumerator SpawnDelay(Vector3 vector3Position, Vector3 vector3Rotation) {
        float timePassed = 0f;
        
        while (timePassed <= spawnDelayTime) {
            timePassed += Time.deltaTime * 1000;
            yield return null;
        }
        aftershockHitbox.transform.position = vector3Position;
        aftershockHitbox.transform.eulerAngles = vector3Rotation;
        aftershockHitbox.SetActive(true);
        //Debug.Log("aftershock shown");

        while (timePassed <= fullAnimationTime) {
            timePassed += Time.deltaTime * 1000;
            yield return null;
        }
        aftershockHitbox.SetActive(false);
        //Debug.Log("aftershock hiddeen");
        aftershockHitbox.transform.localPosition = new Vector3(0, this.gameObject.transform.localPosition.y, 0);
    }

    public void BeginSpawnDelay(Vector3 vector3Position, Vector3 vector3Rotation) {
        //Debug.Log("aftershock started");
        StartCoroutine(SpawnDelay(vector3Position, vector3Rotation));
    }
}
