using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Abilities : MonoBehaviour
{
    /*[Header("Ability 1")]
    public Image ability1Image;
    public Text ability1Text;
    public KeyCode ability1Key;
    public float ability1Cooldown = 5;
    public GameObject abilityProjectile;
    public float ability1Duration = 0.2f;
    public float abiility1Range = 1f;
    public float ability1Speed = 1f;*/

    /*[Header("Ability 2")]
    public Image ability2Image;
    public Text ability2Text;
    public KeyCode ability2Key;
    public float ability2Cooldown = 5;
    public float ability2Duration;*/

    [Header("Ability 3")]
    public Image ability3Image;
    public Text ability3Text;
    public KeyCode ability3Key;
    public float ability3Cooldown = 5;

    private bool isAbility1Cooldown = false;
    private bool isAbility2Cooldown = false;
    private bool isAbility3Cooldown = false;

    private float currentAbility1Cooldown;
    private float currentAbility2Cooldown;
    private float currentAbility3Cooldown;

    PlayerMove playerMove;

    void Start()
    {
        //abilityProjectile.SetActive(false);
        //ability1Image.fillAmount = 0;
        //ability2Image.fillAmount = 0;
        ability3Image.fillAmount = 0;

        //ability1Text.text = "";
        //ability2Text.text = "";
        ability3Text.text = "";
        playerMove = GetComponent<PlayerMove>();
    }

    void Update()
    {
        //Ability1Input();
        //Ability2Input();
        //Ability3Input();

        //AbilityCooldown(ref currentAbility1Cooldown, ability1Cooldown, ref isAbility1Cooldown, ability1Image, ability1Text);
        //AbilityCooldown(ref currentAbility2Cooldown, ability2Cooldown, ref isAbility2Cooldown, ability2Image, ability2Text);
        //AbilityCooldown(ref currentAbility3Cooldown, ability3Cooldown, ref isAbility3Cooldown, ability3Image, ability3Text);
    }

    /*private void Ability1Input()
    {
        if (Input.GetKeyDown(ability1Key) && !isAbility1Cooldown)
        {
            isAbility1Cooldown = true;
            currentAbility1Cooldown = ability1Cooldown;

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                //ROTATION
                Quaternion rotationToLookAt = Quaternion.LookRotation(hit.point - transform.position);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotationToLookAt, 1f);

                //
                Vector3 projectileStartLocation = transform.position;
                Vector3 projectileEndLocation = projectileStartLocation + (abilityProjectile.transform.forward * abiility1Range);
                projectileEndLocation.y = projectileStartLocation.y; //hacky solution to prevent projectile sinking into the ground when cursor is close to player
                //Debug.Log(projectileStartLocation);
                Debug.Log(projectileEndLocation);
                abilityProjectile.SetActive(true);
                StartCoroutine(Ability1(projectileStartLocation, projectileEndLocation));
            }
        }
    }

    IEnumerator Ability1(Vector3 projectileStartLocation, Vector3 projectileEndLocation)
    {
        float distance = Vector3.Distance(projectileStartLocation, projectileEndLocation);
        float time = distance / ability1Speed;
        time = 1/time;
        float currentTimePassed = 0f;
        //Debug.Log("------------" + distance);
        //Debug.Log("------------" + time);
        while (currentTimePassed < 1f)
        {
            Vector3 currentPosition = Vector3.Lerp(projectileStartLocation, projectileEndLocation, currentTimePassed);
            abilityProjectile.transform.position = currentPosition;
            distance = Vector3.Distance(currentPosition, projectileEndLocation);
            currentTimePassed += time * Time.deltaTime; //need to use Time.deltaTime otherwise it will be done in frames, not seconds
            //Debug.Log(distance);
            //Debug.Log(currentTimePassed);
            yield return null;
        }
        abilityProjectile.transform.position = projectileEndLocation;
        abilityProjectile.SetActive(false);
    }*/

/*    private void Ability2Input()
    {
        if (Input.GetKeyDown(ability2Key) && !isAbility2Cooldown)
        {
            isAbility2Cooldown = true;
            currentAbility2Cooldown = ability2Cooldown;

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                //ROTATION
                Quaternion rotationToLookAt = Quaternion.LookRotation(hit.point - transform.position);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotationToLookAt, 1f);


                //MOVEMENT
                StartCoroutine(Ability2(hit));
                //transform.position = Vector3.Lerp(transform.position, hit.point, 0.5f);
            }
        }
    }

    IEnumerator Ability2(RaycastHit hit)
    {
        playerMove.PreventAgentTarget();
        float timeElapsed = 0;
        Vector3 positionBeforeDash = transform.position;
        while (timeElapsed < ability2Duration)
        {
            float travel = timeElapsed / ability2Duration;
            Debug.Log(travel);
            transform.position = Vector3.Lerp(positionBeforeDash, hit.point, travel);
            timeElapsed += Time.deltaTime;

            yield return null;
        }
        transform.position = hit.point;
        //playerMove.AllowAgentTarget();
    }*/

    private void Ability3Input()
    {
        if (Input.GetKeyDown(ability3Key) && !isAbility3Cooldown)
        {
            isAbility3Cooldown = true;
            currentAbility3Cooldown = ability3Cooldown;
        }
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

                if (skillImage != null)
                {
                    skillImage.fillAmount = 0f;
                }
                if (skillText != null)
                {
                    skillText.text = "";
                }
            }
            else
            {
                if (skillImage != null)
                {
                    skillImage.fillAmount = currentCooldown / maxCooldown;
                }
                if (skillText != null)
                {
                    skillText.text = Mathf.Ceil(currentCooldown).ToString();
                }
            }
        }
    }
}
