using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterTemplate : MonoBehaviour
{
    protected CharacterInfo characterInfo;
    protected PlayerMove playerMove;
    //protected EnemyInteraction enemyInteraction;

    protected void Awake() {
        characterInfo = transform.root.GetComponent<CharacterInfo>();
        playerMove = transform.root.GetComponent<PlayerMove>();
        //enemyInteraction = transform.root.gameObject.GetComponent<EnemyInteraction>();
    }
}
