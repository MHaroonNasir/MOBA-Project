using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterTemplate : MonoBehaviour
{
    protected GenericCharacterStatsAndActions genericStatsAndActions;
    protected PlayerMove playerMove;
    protected EnemyInteraction enemyInteraction;

    protected void Awake() {
        genericStatsAndActions = transform.root.GetComponent<GenericCharacterStatsAndActions>();
        playerMove = transform.root.GetComponent<PlayerMove>();
        enemyInteraction = transform.root.gameObject.GetComponent<EnemyInteraction>();
    }
    
}
