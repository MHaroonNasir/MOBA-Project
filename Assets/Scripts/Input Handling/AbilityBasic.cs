using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class AbilityBasic : CombatSystem
{
    //input
    public InputAction abilityKey;
    public InputAction cancelAbility;

    //cooldown
    public float cooldownDuration = 5f;
    public static bool isOnCooldown = false;
    private float currentCooldown;

    //current position
    private Vector3 position;

    //cooldown canvas
    public Image abilityImageGreyed;
    public Text abilityCooldownText;

    //range indicator canvas
    //this has to be handled by events as abilities are unique and have unique canvases???
    public Canvas abilityCanvas;
    public Image abilityRangeIndicator;

    void Start()
    {
        abilityImageGreyed.fillAmount = 0;
        abilityCooldownText.text = "";

        abilityRangeIndicator.enabled = false;
    }

    void Update()
    {
        AbilityInput();
        AbilityCooldown(ref currentCooldown, cooldownDuration, ref isOnCooldown, abilityImageGreyed, abilityCooldownText);
    }

    private void AbilityInput()
    {
        if (abilityKey.IsPressed()) {
            //activate canvas

            abilityRangeIndicator.enabled = true;
            AbilityCanvasOn();
        }
        if (abilityKey.IsPressed() && cancelAbility.WasPerformedThisFrame()) {
            //cancel ability
            AbilityCanvasOff();
        }
        if (abilityKey.WasReleasedThisFrame() && !isOnCooldown) {
            //disable canvas and perform action
            AbilityCasted();

            AbilityCanvasOff();
        }
    }

    private void AbilityCanvasOn()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (abilityRangeIndicator.enabled)
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            }

            Quaternion abCanvas = Quaternion.LookRotation(position - transform.position);
            abCanvas.eulerAngles = new Vector3(0, abCanvas.eulerAngles.y, abCanvas.eulerAngles.z);
            abilityCanvas.transform.rotation = Quaternion.Lerp(abCanvas, abilityCanvas.transform.rotation, 0); 
        }
    }

    void AbilityCasted() {
        if (abilityRangeIndicator.enabled)
        {
            isOnCooldown = true;
            currentCooldown = cooldownDuration;

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity)) {
                //ROTATION
                //Quaternion rotationToLookAt = Quaternion.LookRotation(hit.point - transform.position);
                //transform.rotation = Quaternion.Lerp(transform.rotation, rotationToLookAt, 1f);
                playerMove.RotateCharacter(hit.point);

                //StartCoroutine(Ability4Cast());
                stats.ID.ability1?.Invoke();
            }
        }
    }

    private void AbilityCooldown(ref float currentCooldown, float maxCooldown, ref bool isCooldown, Image skillImage, Text skillText)
    {
        if (isCooldown) {
            currentCooldown -= Time.deltaTime;

            if (currentCooldown <= 0f) {
                isCooldown = false;
                currentCooldown = 0f;

                if (skillImage != null) { skillImage.fillAmount = 0f; }
                if (skillText != null) { skillText.text = ""; }
            }
            else {
                if (skillImage != null) { skillImage.fillAmount = currentCooldown / maxCooldown; }
                if (skillText != null) { skillText.text = Mathf.Ceil(currentCooldown).ToString(); }
            }
        }
    }

    void AbilityCanvasOff() {
        abilityRangeIndicator.enabled = false;
    }

    private void OnEnable() {
        abilityKey.Enable();
        cancelAbility.Enable();
        //abilityRangeIndicator.enabled = false;
    }

    private void OnDisable() {
        abilityKey.Disable();
        cancelAbility.Disable();
        //abilityRangeIndicator.enabled = false;
    }
}
