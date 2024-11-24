using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class Ability5 : CombatSystem
{
    [Header("Ability 5")]
    public Image ability5Image;
    public Text ability5Text;
    public InputAction ability5Key;
    public float ability5Cooldown = 5;

    public static bool isAbility5Cooldown = false;
    private float currentAbility5Cooldown;

    public Canvas ability5Canvas;
    public Image ability5Cone;

    private Vector3 position;

    public PlayerAbilityCast ability5IndicatorActive;
    bool ability5CurrentlyActive = false;
    bool[] abilityCurrentlyActive = PlayerMove.abilityCurrentlyActive;
    
    void Start()
    {
        abilityCurrentlyActive[5] = ability5CurrentlyActive;
        ability5Key.Enable();
        ability5Image.fillAmount = 0;
        ability5Text.text = "";

        ability5Cone.enabled = false;
        ability5Canvas.enabled = false;
    }

    void Update()
    {
        //Ability5Input();
        AbilityCooldown(ref currentAbility5Cooldown, ability5Cooldown, ref isAbility5Cooldown, ability5Image, ability5Text);
    }

    private void Ability5Input()
    {
        if (ability5Key.IsPressed())
        {
            ability5IndicatorActive.Invoke(5, true);
        }
        if (ability5Key.IsPressed() && AbilityCast.previousId == 5)
        {
            //Debug.Log("5 called");
            ability5IndicatorActive.Invoke(5, true);
            //Debug.Log(abilityCurrentlyActive[5]);
            ability5Canvas.enabled = true;
            ability5Cone.enabled = true; 
            Ability5CanvasOn();
        }
        if (ability5Key.WasReleasedThisFrame() && !isAbility5Cooldown)
        {
            //Debug.Log("5 deactivate");
            ability5IndicatorActive.Invoke(5, false);
            ability5Canvas.enabled = false;
            ability5Cone.enabled = false; 

            isAbility5Cooldown = true;
            currentAbility5Cooldown = ability5Cooldown;
        }
        /*ability5IndicatorActive.Invoke(5, true);
        if (abilityCurrentlyActive[4] != true)
        if (previousId == 5)
        {
            if (ability5Key.IsPressed())
            {
                Debug.Log("5 called");
                ability5IndicatorActive.Invoke(5, true);
                Debug.Log(abilityCurrentlyActive[5]);
                ability5Canvas.enabled = true;
                ability5Cone.enabled = true; 
                Ability5CanvasOn();
            }
            if (ability5Key.WasReleasedThisFrame() && !isAbility5Cooldown)
            {
                Debug.Log("5 deactivate");
                ability5IndicatorActive.Invoke(5, false);
                ability5Canvas.enabled = false;
                ability5Cone.enabled = false; 

                isAbility5Cooldown = true;
                currentAbility5Cooldown = ability5Cooldown;
            }
        }*/
    }

    public void Ability5CastCanvas()
    {
        ability5Canvas.enabled = true;
        ability5Cone.enabled = true; 
        Ability5CanvasOn();
    }

    public void Ability5DisableCanvas()
    {
        ability5Canvas.enabled = false;
        ability5Cone.enabled = false; 
    } 

    private void Ability5CanvasOn()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (ability5Cone.enabled)
        //if (ability4Canvas.enabled)
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            }

            Quaternion ab5Canvas = Quaternion.LookRotation(position - transform.position);
            ab5Canvas.eulerAngles = new Vector3(0, ab5Canvas.eulerAngles.y, ab5Canvas.eulerAngles.z);
            ability5Canvas.transform.rotation = Quaternion.Lerp(ab5Canvas, ability5Canvas.transform.rotation, 0); 
        }
    }
    
    public void Ability5Casted()
    {
        ability5IndicatorActive.Invoke(5, false);

        if (ability5Canvas.enabled)
        {
            isAbility5Cooldown = true;
            currentAbility5Cooldown = ability5Cooldown;
        }

        ability5Canvas.enabled = false;
        ability5Cone.enabled = false; 
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

    private void DisableAbility5Key()
    {
        ability5Key.Disable();
    }

    private void Notify()
    {
        Debug.Log("4 key INVOKED");
    }

    private void OnEnable() {
        //stats.ID.testAction += Notify;
        stats.ID.sweepAttack += Notify;
    }

    private void OnDisable() {
        stats.ID.sweepAttack -= Notify;
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
