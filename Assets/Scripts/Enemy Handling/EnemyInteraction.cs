using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(PlayerMove))]
[RequireComponent(typeof(AutoAttackScript))]
//script should be such that removing it prevents enemy targeting, like lobby inbetween matches of 2v2 arena mode
public class EnemyInteraction : CombatSystem
{
    NavMeshAgent agent;
    AutoAttackScript autoAttackScript;
    //PlayerMove playerMove;
    Animator animator;
    public InputAction mouseRightClick;

    public LayerMask enemyLayerMask;
    public DamageCalculation enemyDamageCalculation;
    private HighlightManager highlightManager; //could leave out or in

    public GameObject targetEnemy;
    public GameObject storedTargetEnemy; //temporary storage for when ability is casted, after which this values returns to targetEnemy

    private void Start() {
        agent = GetComponent<NavMeshAgent>();
        autoAttackScript = GetComponent<AutoAttackScript>();
        //playerMove = GetComponent<PlayerMove>();
        animator = GetComponent<Animator>();

        highlightManager = GetComponent<HighlightManager>();
    }

    private void Update() {
        Move();
    }

    void Move() {
        if (mouseRightClick.WasPerformedThisFrame()) {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity)) {
                if (hit.transform.gameObject.tag == "Enemy") { 
                    targetEnemy = hit.collider.gameObject;
                    enemyDamageCalculation = targetEnemy.GetComponent<DamageCalculation>();
                    MoveTowardsenemy();     //also works
                }
            }
        }
        if (targetEnemy != null) {
            MoveTowardsenemy();
            autoAttackScript.CheckEnemyInAutoAttackRange();
        } else {
            autoAttackScript.EndAutoAttackAnimation();
        }
    }

    void MoveTowardsenemy()
    {
        //targetEnemy = enemy;
        agent.SetDestination(targetEnemy.transform.position); //look up difference between setdestination and destination
        agent.stoppingDistance = stats.ID.attackRange;
        /*float distanceToTarget = Vector3.Distance(targetEnemy.transform.position, transform.position);
        if (distanceToTarget <= stats.ID.attackRange * 1.1) {
            AttackAnimation(true);
        }*/

        this.playerMove.Rotation(targetEnemy.transform.position);
        highlightManager.SelectedHighlight();
    }

    void StoreEnemyTarget() {
        storedTargetEnemy = targetEnemy;
        targetEnemy = null;
    }

    void ResumeEnemyTarget() {
        targetEnemy = storedTargetEnemy;
        storedTargetEnemy = null;
    }

    void ResetEnemyTargeting() {
        //AttackAnimation(false);
        if (targetEnemy != null) //this function only called when user makes non-aggressive moves and does not target an enemy, hence the any enemy currently targeted and highlighted should be removed
        {
            //Debug.Log("TARGET ENEMY NOW NULL");
            highlightManager.DeselectHighlight();
            targetEnemy = null;
        }
    }

    /*void AttackAnimation(bool attackState) {
        animator.SetBool("Attacking", attackState);
    }*/

    void OnEnable() {
        stats.ID.holdEnemyTargetting += StoreEnemyTarget;
        stats.ID.returnEnemyTargetting += ResumeEnemyTarget;
        stats.ID.resetEnemyTargeting += ResetEnemyTargeting;
        mouseRightClick.Enable();
    }

    void OnDisable() {
        stats.ID.holdEnemyTargetting -= StoreEnemyTarget;
        stats.ID.returnEnemyTargetting -= ResumeEnemyTarget;
        stats.ID.resetEnemyTargeting -= ResetEnemyTargeting;
        mouseRightClick.Disable();
    }
}
