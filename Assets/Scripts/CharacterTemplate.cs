using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterTemplate : MonoBehaviour
{
    protected SamuraiCharacterInfo samuraiCharacterInfo;
    protected PlayerMove playerMove;
    //protected EnemyInteraction enemyInteraction;

    protected void Awake() {
        samuraiCharacterInfo = transform.root.GetComponent<SamuraiCharacterInfo>();
        playerMove = transform.root.GetComponent<PlayerMove>();
        //enemyInteraction = transform.root.gameObject.GetComponent<EnemyInteraction>();
    }
    
}
