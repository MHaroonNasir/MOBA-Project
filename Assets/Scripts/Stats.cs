using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Stats : MonoBehaviour
{
    public PlayerID ID;

    NavMeshAgent navMeshAgent;
    Animator animator;

    private void Awake() {
        //ID = transform.root.GetComponent<PlayerID>();
    }

    private void Start() {
        TryGetComponent<NavMeshAgent>(out NavMeshAgent navMeshAgent);
        navMeshAgent.speed = ID.movementSpeedBase;
        TryGetComponent<Animator>(out Animator animator);
        animator.SetFloat("Attack Speed", ID.appliedAttackSpeed);
    }

    public void UpdateMovementSpeed(float value)
    {
        navMeshAgent.speed = value;
    }

    public void UpdateAttackSpeed(float value)
    {
        animator.SetFloat("Attack Speed", value);
    }
}
