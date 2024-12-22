using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyInteraction))]
public class AutoAttackScript : CombatSystem
{
    //PlayerMove playerMove;
    //EnemyInteraction enemyInteraction;
    DamageCalculation damageCalculation;
    Animator animator;
    [SerializeField] float autoAttackDamage = 10f;

    private void Awake() {
        base.Awake();
    }
    
    void Start()
    {
        //playerMove = GetComponent<PlayerMove>();
        //enemyInteraction = GetComponent<EnemyInteraction>();
        damageCalculation = GetComponent<DamageCalculation>();
        animator = GetComponent<Animator>();
        this.playerMove.EnableMovement();
    }

    /*void Update() {
        //need to have a target in order to auto attack
        if (enemyInteraction.targetEnemy != null) {
            //Debug.Log("target ot null");
            CheckEnemyInAutoAttackRange();
        } else {
            EndAutoAttackAnimation();
        }
    }*/

    public void CheckEnemyInAutoAttackRange() {
        playerMove.agent.stoppingDistance = stats.ID.appliedAttackRange;
        float distanceToTarget = Vector3.Distance(enemyInteraction.targetEnemy.transform.position, transform.position);
        //Debug.Log(distanceToTarget);
        if (distanceToTarget <= stats.ID.appliedAttackRange * 1.015) { //added micro length to auto as stopping distance in EnemyInteraction can be slightly above the value mentioned
            //Debug.Log("started auto");
            StartAutoAttackAnimation();
        }
    }

    //called by animation
    public void StartAutoAttackGuaranteeLock()
    {
        this.playerMove.DisableMovement();
        //increase stopping distance to infinity
        //there may be a bug where is user goes out of range when this method called but not EndAutoAttack, the player is stuck uncontrollably chasing the enemy
    }

    //called by animation
    public void DealAutoAttackDamage()
    {
        //playermove stores enemy damagecalculation script
        //call the receievedamage function of that script and pass in return value of this objects own calculatedamage function
        enemyInteraction.enemyDamageCalculation.ReceiveDamage(damageCalculation.CalculateDamage(autoAttackDamage));
    }

    //called by animation
    public void EndAutoAttackGuaranteeLock()
    {
        this.playerMove.EnableMovement();
    }

    void StartAutoAttackAnimation() {
        float distanceToTarget = Vector3.Distance(enemyInteraction.targetEnemy.transform.position, transform.position);
        if (distanceToTarget <= stats.ID.appliedAttackRange * 1.1) {
            //Debug.Log("auto in range");
            animator.SetBool("Attacking", true);
        }
    }

    public void EndAutoAttackAnimation() {
        animator.SetBool("Attacking", false);
    }

    private void OnEnable() {
        stats.ID.ability1 += EndAutoAttackAnimation;
    }

    private void OnDisable() {
        stats.ID.ability1 -= EndAutoAttackAnimation;
    }
}
