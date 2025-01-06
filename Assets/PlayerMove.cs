using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.Events;
using System;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMove : CharacterTemplate
{
    public Transform goal;
    public Camera camera;
    public InputAction mouseRightClick;

    public NavMeshAgent agent;
    public float rotateSpeedMovement;
    private float rotateVelocity;
    public Animator anim;
    float motionSmoothTime = 0.1f;

    public static bool[] abilityCurrentlyActive = new bool[7]; //0 reserved for passive
    //public static int previousId = 0;

    [Header("Enemy Targeting")]
    public LayerMask enemyLayerMask;
    public GameObject targetEnemy;
    //public DamageCalculation enemyDamageCalculation;
    public float stoppingDistance;
    //private HighlightManager highlightManager;

    private void Awake() {
        base.Awake();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start() 
    {
        //highlightManager = GetComponent<HighlightManager>();
        //agent.destination = goal.position;
        mouseRightClick.Enable();
    }

    void Update()
    {
        MoveAnimation();
        Move();
    }

    public void Move()
    {
        if (mouseRightClick.WasPerformedThisFrame())
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                //MOVEMENT
                //if (hit.collider.tag == "Ground") {   //also works
                if (hit.transform.gameObject.tag == "Ground") { 
                    //Debug.Log("MOVE");
                    MoveToPosition(hit.point);
                    //Debug.Log(hit.point);
                }
                //if (hit.collider.CompareTag("Enemy")) {   //also works
                /*else if (hit.transform.gameObject.tag == "Enemy") { 
                    //Debug.Log("TARGET ENEMY SET");
                    targetEnemy = hit.collider.gameObject;
                    enemyDamageCalculation = targetEnemy.GetComponent<DamageCalculation>();
                    MoveTowardsenemy(targetEnemy);     //also works
                    //agent.stoppingDistance = attackRange;
                }*/
            }
        }

        /*if (targetEnemy != null) {
            MoveTowardsenemy(targetEnemy);
        }*/
        //Debug.Log(transform.position);
    }

    public void MoveToPosition(Vector3 position)
    {
        agent.SetDestination(position); //look up difference between setdestination and destination
        agent.stoppingDistance = 0;
        RotateCharacter(position);
        characterInfo.genericStatsAndActions.resetEnemyTargeting?.Invoke();
    }

    public void RotateCharacter(Vector3 lookAtPosition) {
        //ROTATION
        Quaternion rotationToLookAt = Quaternion.LookRotation(lookAtPosition - transform.position);
        float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationToLookAt.eulerAngles.y, 
            ref rotateVelocity, rotateSpeedMovement * (Time.deltaTime * 5));
        transform.eulerAngles = new Vector3(0, rotationY, 0);
    }

    public void MoveAnimation() {
        float speed = agent.velocity.magnitude / agent.speed;
        anim.SetFloat("Speed", speed, motionSmoothTime, Time.deltaTime);
    }

    public void UpdateAgentSpeed() {
        double baseMovementSpeed = characterInfo.genericStatsAndActions.movementSpeedBase;
        double flatMoveSpeedIncrease = characterInfo.genericStatsAndActions.movementSpeedFlatModification;
        double percentMoveSpeedAdjust = characterInfo.genericStatsAndActions.movementSpeedPercentModification;

        characterInfo.genericStatsAndActions.movementSpeedApplied = Math.Round((baseMovementSpeed + flatMoveSpeedIncrease) * percentMoveSpeedAdjust, 2);
        agent.speed = (float)characterInfo.genericStatsAndActions.movementSpeedApplied;
    }

    public void AllowAgentTarget()
    {
        agent.updatePosition = true;
    }

    public void PreventAgentTarget()
    {
        agent.ResetPath();
    }

    public void EnableMovement() {
        mouseRightClick.Enable();
    }

    public void DisableMovement() {
        mouseRightClick.Disable();
    }

    private void OnEnable() {
        //stats.ID.isSlowed += UpdateMovementSpeed;
        //stats.ID.ccEnded += UpdateMovementSpeed;
        characterInfo.genericStatsAndActions.updateMovementSpeed += UpdateAgentSpeed;
    }

    private void OnDisable() {
        //stats.ID.isSlowed -= UpdateMovementSpeed;
        //stats.ID.ccEnded -= UpdateMovementSpeed;
        characterInfo.genericStatsAndActions.updateMovementSpeed -= UpdateAgentSpeed;
    }
}

[System.Serializable]
public class PlayerAbilityCast : UnityEvent<int, bool> {}
