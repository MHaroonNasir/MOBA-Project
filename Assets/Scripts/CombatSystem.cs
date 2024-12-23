using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CombatSystem : MonoBehaviour
{
    protected Stats stats;
    protected PlayerMove playerMove;
    protected EnemyInteraction enemyInteraction;

    protected void Awake() {
        stats = transform.root.GetComponent<Stats>();
        playerMove = transform.root.GetComponent<PlayerMove>();
        enemyInteraction = transform.root.gameObject.GetComponent<EnemyInteraction>();
    }
}
