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
        //agent.speed = stats.ID.movementSpeedApplied;
    }

    void Update()
    {
        MoveAnimation();
        Move();
        //anim.SetFloat("Attack Speed", attackSpeed);
        /*if (mouseRightClick.WasPressedThisFrame())
        {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Debug.Log(hit.point);
                if (hit.transform.gameObject.tag == "Ground") { Move(hit.point); }
                else if (hit.transform.gameObject.tag == "Enemy") { Attack(hit.point); }
                
                //agent.destination = hit.point;
            }
        }*/
    }

    /*void Move(Vector3 destinationPoint)
    {
        Debug.Log("Move");
        agent.destination = destinationPoint;
        agent.stoppingDistance = 0;
    }*/

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
        //AttackAnimation(false);
        agent.SetDestination(position); //look up difference between setdestination and destination
        agent.stoppingDistance = 0;
        //Debug.Log(agent.destination);

        Rotation(position);

        //stats.ID.resetEnemyTargeting?.Invoke();
        characterInfo.genericStatsAndActions.resetEnemyTargeting?.Invoke();

        /*if (targetEnemy != null) //this function only called when user makes non-aggressive moves and does not target an enemy, hence the any enemy currently targeted and highlighted should be removed
        {
            //Debug.Log("TARGET ENEMY NOW NULL");
            highlightManager.DeselectHighlight();
            targetEnemy = null;
        }*/
    }

    /*public void MoveTowardsenemy(GameObject enemy)
    {
        //targetEnemy = enemy;
        agent.SetDestination(targetEnemy.transform.position); //look up difference between setdestination and destination
        agent.stoppingDistance = stats.ID.attackRange;
        float distanceToTarget = Vector3.Distance(targetEnemy.transform.position, transform.position);
        if (distanceToTarget <= stats.ID.attackRange * 1.1)
        {
            AttackAnimation(true);
        }

        Rotation(targetEnemy.transform.position);
        highlightManager.SelectedHighlight();
    }*/

    public void Rotation(Vector3 lookAtPosition) {
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
        //agent.speed = stats.ID.movementSpeedApplied;
        characterInfo.genericStatsAndActions.movementSpeedApplied = (characterInfo.genericStatsAndActions.movementSpeedBase + characterInfo.genericStatsAndActions.movementSpeedFlatModification) 
            * characterInfo.genericStatsAndActions.movementSpeedPercentModification;
        agent.speed = characterInfo.genericStatsAndActions.movementSpeedApplied;
        //Debug.Log("updated mvoement speed: " + stats.ID.movementSpeedApplied);
    }

    /*public void AttackAnimation(bool attackState)
    {
        anim.SetBool("Attacking", attackState);
    }*/

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
