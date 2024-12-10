using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowHitbox : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
        other.gameObject.TryGetComponent<CCSlow>(out CCSlow ccSlow);
        ccSlow.ReceiveCCSlow(2.5f, '%', 0.65f);
    }
}
