using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ability2 : MonoBehaviour
{
    [Header("Ability 2")]
    public Image ability2Image;
    public Text ability2Text;
    public KeyCode ability2Key;
    public float ability2Cooldown = 5;
    public float ability2Duration;

    private bool isAbility2Cooldown = false;

    private float currentAbility2Cooldown;

    PlayerMove playerMove;

    public PlayerAbilityCast currentlyDashing;
    bool ability2CurrentlyActive = false;
    bool[] abilityCurrentlyActive = PlayerMove.abilityCurrentlyActive;

    void Start()
    {
        abilityCurrentlyActive[2] = ability2CurrentlyActive;
        ability2Image.fillAmount = 0;
        ability2Text.text = "";
        playerMove = GetComponent<PlayerMove>();
    }

    void Update()
    {
        Ability2Input();
        AbilityCooldown(ref currentAbility2Cooldown, ability2Cooldown, ref isAbility2Cooldown, ability2Image, ability2Text);
    }

    private void Ability2Input()
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
                StartCoroutine(Ability2Cast(hit));
                //transform.position = Vector3.Lerp(transform.position, hit.point, 0.5f);
            }
        }
    }

    IEnumerator Ability2Cast(RaycastHit hit)
    {
        playerMove.PreventAgentTarget();
        float timeElapsed = 0;
        Vector3 positionBeforeDash = transform.position;
        currentlyDashing.Invoke(2, true);
        while (timeElapsed < ability2Duration)
        {
            float travel = timeElapsed / ability2Duration;
            //Debug.Log(travel);
            transform.position = Vector3.Lerp(positionBeforeDash, hit.point, travel);
            timeElapsed += Time.deltaTime;

            yield return null;
        }
        currentlyDashing.Invoke(2, false);
        transform.position = hit.point;
        //playerMove.AllowAgentTarget();
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

    public void toggleAbilityCurrentlyActive(int abilityId, bool currentlyActive)
    {
        abilityCurrentlyActive[abilityId] = currentlyActive;
    }
}

