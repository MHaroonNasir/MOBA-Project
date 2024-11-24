using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class Ability4 : CombatSystem
{
    [Header("Ability 4")]
    public Image ability4Image;
    public Text ability4Text;
    //public KeyCode ability4Key;
    public InputAction ability4Key;
    public float ability4Cooldown = 5;
    public GameObject abilityProjectile;
    //public float ability4Duration = 0.2f;
    public float abiility4Range = 1f;
    public float ability4Speed = 1f;

    public static bool isAbility4Cooldown = false;
    private float currentAbility4Cooldown;

    public Canvas ability4Canvas;
    public Image ability4Skillshot;

    private Vector3 position;
    //private RaycastHit hit;
    //private Ray ray;

    public PlayerAbilityCast ability4IndicatorActive;
    bool ability4CurrentlyActive = false;
    bool[] abilityCurrentlyActive = PlayerMove.abilityCurrentlyActive;

    private void Awake() {
        base.Awake();
    }

    void Start()
    {
        abilityCurrentlyActive[4] = ability4CurrentlyActive;
        ability4Key.Enable();
        ability4Image.fillAmount = 0;
        ability4Text.text = "";

        ability4Skillshot.enabled = false;
        ability4Canvas.enabled = false;
    }

    void Update()
    {
        //Ability4Input();
        AbilityCooldown(ref currentAbility4Cooldown, ability4Cooldown, ref isAbility4Cooldown, ability4Image, ability4Text);
    }

    private void Ability4Input()
    {
        if (ability4Key.IsPressed())
        {
            ability4IndicatorActive.Invoke(4, true);
        }
        if (ability4Key.IsPressed() && AbilityCast.previousId == 4)
        {
            ability4IndicatorActive.Invoke(4, true);
            ability4Canvas.enabled = true;
            ability4Skillshot.enabled = true; 
            Ability4CanvasOn();
        }
        if (ability4Key.WasReleasedThisFrame() && !isAbility4Cooldown)
        {
            ability4IndicatorActive.Invoke(4, false);
            ability4Canvas.enabled = false;
            ability4Skillshot.enabled = false; 

            isAbility4Cooldown = true;
            currentAbility4Cooldown = ability4Cooldown;
        }

        //ability4IndicatorActive.Invoke(4, true);
        //if (abilityCurrentlyActive[5] != true)
        /*if (previousId == 4)
        {
            Debug.Log(abilityCurrentlyActive[5]);
            if (ability4Key.IsPressed())
            {
                Debug.Log("4 called");
                ability4IndicatorActive.Invoke(4, true);
                ability4Canvas.enabled = true;
                ability4Skillshot.enabled = true; 
                Ability4CanvasOn();
            }
            if (ability4Key.WasReleasedThisFrame() && !isAbility4Cooldown)
            {
                Debug.Log("4 deactivate");
                ability4IndicatorActive.Invoke(4, false);
                ability4Canvas.enabled = false;
                ability4Skillshot.enabled = false; 

                isAbility4Cooldown = true;
                currentAbility4Cooldown = ability4Cooldown;

                RaycastHit hit;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
                {
                    //ROTATION
                    Quaternion rotationToLookAt = Quaternion.LookRotation(hit.point - transform.position);
                    transform.rotation = Quaternion.Lerp(transform.rotation, rotationToLookAt, 1f);

                    StartCoroutine(Ability4Cast());
                }
            }
        }*/
    }

    public void Ability4CastCanvas()
    {
        ability4Canvas.enabled = true;
        ability4Skillshot.enabled = true; 
        Ability4CanvasOn();
    }

    public void Ability4DisableCanvas()
    {
        ability4Canvas.enabled = false;
        ability4Skillshot.enabled = false; 
    }

    private void Ability4CanvasOn()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (ability4Skillshot.enabled)
        //if (ability4Canvas.enabled)
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            }

            Quaternion ab4Canvas = Quaternion.LookRotation(position - transform.position);
            ab4Canvas.eulerAngles = new Vector3(0, ab4Canvas.eulerAngles.y, ab4Canvas.eulerAngles.z);
            ability4Canvas.transform.rotation = Quaternion.Lerp(ab4Canvas, ability4Canvas.transform.rotation, 0); 
        }
    }

    public void Ability4Casted()
    {
        ability4IndicatorActive.Invoke(4, false);

        if (ability4Canvas.enabled)
        {
            isAbility4Cooldown = true;
            currentAbility4Cooldown = ability4Cooldown;

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                //ROTATION
                Quaternion rotationToLookAt = Quaternion.LookRotation(hit.point - transform.position);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotationToLookAt, 1f);

                StartCoroutine(Ability4Cast());
            }
        }
        ability4Canvas.enabled = false;
        ability4Skillshot.enabled = false; 
    }

    IEnumerator Ability4Cast()
    {
        Vector3 projectileStartLocation = transform.position;
        Vector3 projectileEndLocation;
        if (abilityCurrentlyActive[2]) { projectileEndLocation = (projectileStartLocation + (abilityProjectile.transform.forward * abiility4Range)) * 4; } //while dashing, double range
        else { projectileEndLocation = (projectileStartLocation + (abilityProjectile.transform.forward * abiility4Range)) * 1; }                         //while not dashing, range normal
        //Vector3 projectileEndLocation = projectileStartLocation + (abilityProjectile.transform.forward * abiility4Range);
        projectileEndLocation.y = projectileStartLocation.y; //hacky solution to prevent projectile sinking into the ground when cursor is close to player
        //Debug.Log(projectileStartLocation);
        //Debug.Log(projectileEndLocation);
        abilityProjectile.SetActive(true);

        float distance = Vector3.Distance(projectileStartLocation, projectileEndLocation);
        float time = distance / ability4Speed;
        time = 1/time;
        float currentTimePassed = 0f;
        //Debug.Log("------------" + distance);
        //Debug.Log("------------" + time);
        while (currentTimePassed < 1f)
        {
            Vector3 currentPosition = Vector3.Lerp(projectileStartLocation, projectileEndLocation, currentTimePassed);
            abilityProjectile.transform.position = currentPosition;
            //distance = Vector3.Distance(currentPosition, projectileEndLocation);
            currentTimePassed += time * Time.deltaTime; //need to use Time.deltaTime otherwise it will be done in frames, not seconds
            //Debug.Log(distance);
            //Debug.Log(currentTimePassed);
            yield return null;
        }
        abilityProjectile.transform.position = projectileEndLocation;
        abilityProjectile.SetActive(false);
    }

    private void AbilityCooldown(ref float currentCooldown, float maxCooldown, ref bool isCooldown, Image skillImage, Text skillText)
    {
        if (isCooldown)
        {
            currentCooldown -= Time.deltaTime;

            if (currentCooldown <= 0f)
            {
                isCooldown = false;
                currentCooldown = 0f;

                if (skillImage != null) { skillImage.fillAmount = 0f; }
                if (skillText != null) { skillText.text = ""; }
            }
            else
            {
                if (skillImage != null) { skillImage.fillAmount = currentCooldown / maxCooldown; }
                if (skillText != null) { skillText.text = Mathf.Ceil(currentCooldown).ToString(); }
            }
        }
    }

    private void DisableAbility4Key()
    {
        ability4Key.Disable();
    }

    private void Notify()
    {
        Debug.Log("4 key INVOKED");
    }

    private void OnEnable() {
        //stats.ID.testAction += Notify;
        stats.ID.events.testAction += Notify;
    }

    private void OnDisable() {
        stats.ID.events.testAction -= Notify;
    }

    //int previousId = PlayerMove.previousId;
    /*public void toggleAbilityCurrentlyActive(int abilityId, bool currentlyActive)
    {
        abilityCurrentlyActive[abilityId] = currentlyActive;
        if (abilityId != PlayerMove.previousId) 
        { 
            PlayerMove.previousId = abilityId; 
            Debug.Log(PlayerMove.previousId);
        };
    }*/
}
