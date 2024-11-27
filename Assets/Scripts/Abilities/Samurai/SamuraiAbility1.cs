using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiAbility1 : CombatSystem
{
    [SerializeField] GameObject ability1Hitbox; //research difference between serializefield and public
    public SpawnAfterDelay spawnAfterDelay;
    public HitboxTest hitboxTest; //script needs renaming
    Animator animator;
    //PlayerMove playerMove;

    //Transform transform;

    public float forwardDistance = 3f;
    public static int dashTravelFrames = 62;
    public static int fullAnimationFrames = 120;
    float dashTravelTime = dashTravelFrames * 1000 / 60; //27f - 27/60 as 60f = 1000ms = 1s --- 27 frames is hardcoded time travel distance in ability1 animation
    public float fullAnimationTime = fullAnimationFrames * 1000 / 60;

    int ability1Stack = 0;

    private void Awake() {
        base.Awake();
    }

    void Start()
    {
        //transform = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        //playerMove = GetComponent<PlayerMove>();
    }

    IEnumerator Dash() {
        float timePassed = 0f;
        Vector3 positionBeforeDash = transform.position;
        Vector3 destinationCoords = positionBeforeDash + (transform.forward * forwardDistance);
        //Debug.Log(transform.position + " " + destinationCoords);
        //playerMove.DisableMovement();
        this.playerMove.DisableMovement(); //from combat system parent class, this keyword not needed
        this.playerMove.PreventAgentTarget();

        while (timePassed < dashTravelTime)
        {
            Vector3 newPosition = Vector3.Lerp(positionBeforeDash, destinationCoords, timePassed/dashTravelTime);
            //Debug.Log(timePassed + " " + newPosition);
            transform.position = newPosition;
            //Debug.Log(timePassed + " " + transform.position);
            timePassed += Time.deltaTime * 1000; //need to use Time.deltaTime otherwise it will be done in frames, not seconds
            //Debug.Log(timePassed + " " + Time.deltaTime);
            yield return null;
        }
        this.playerMove.EnableMovement(); //from combat system parent class, this keyword not needed
        stats.ID.returnEnemyTargetting?.Invoke();
        spawnAfterDelay.BeginSpawnDelay(this.transform.position + (transform.forward * forwardDistance), this.gameObject.transform.rotation.eulerAngles);
        //animator.ResetTrigger("Ability1");
        //Debug.Log("finishi ability1");
        //animator.SetTrigger("test trigger");
    }

    IEnumerator CancelAbilityRecoveryAnimation() {
        float remainingAnimationTime = fullAnimationTime;
        //Debug.Log(remainingAnimationTime);
        while (remainingAnimationTime > 0) {
            remainingAnimationTime -= Time.deltaTime * 1000;
            //Debug.Log(remainingAnimationTime);
            if (this.enemyInteraction.targetEnemy != null || playerMove.agent.hasPath != false) {
                animator.SetTrigger("test trigger");
                StopCoroutine(CancelAbilityRecoveryAnimation());
                //Debug.Log("=ability1 cacelled reecoery");
            } else {
                yield return null;
            }
        }
        animator.SetTrigger("test trigger");
        //Debug.Log("ability1 fully ended");
        Debug.Log("ability 1 stack: " + ability1Stack);
    }

    void IncreaseAbilityStacks() {
        ability1Stack = ability1Stack + 1 > 2 ? 2 : ability1Stack + 1;
    }

    void DecreaseAbilityStacks() {
        ability1Stack = ability1Stack - 1 < 0 ? 0 : ability1Stack - 1;
    }

    void CastAbility1() {
        stats.ID.holdEnemyTargetting?.Invoke();
        animator.SetTrigger("Ability1");
        StartCoroutine(Dash());
        StartCoroutine(CancelAbilityRecoveryAnimation());
    }

    private void OnEnable() {
        stats.ID.ability1 += CastAbility1;
        hitboxTest.SwordHitEnemy += IncreaseAbilityStacks;
        hitboxTest.SwordNotHitEnemy += DecreaseAbilityStacks;
    }

    private void OnDisable() {
        stats.ID.ability1 -= CastAbility1;
        hitboxTest.SwordHitEnemy -= IncreaseAbilityStacks;
        hitboxTest.SwordNotHitEnemy -= DecreaseAbilityStacks;
    }
}
