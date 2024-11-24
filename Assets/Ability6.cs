using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Ability6 : MonoBehaviour
{
    [Header("Ability 6")]
    public Image ability6Image;
    public Text ability6Text;
    public InputAction ability6Key;
    public float ability6Cooldown = 5;

    private bool isAbility6Cooldown = false;
    private float currentAbility6Cooldown;

    public Canvas ability6Canvas;
    public Image ability6RangeIndicator;
    public float maxAbility6Distance = 7;

    private Vector3 position;

    bool ability6CurrentlyActive = false;
    bool[] abilityCurrentlyActive = PlayerMove.abilityCurrentlyActive;
    
    void Start()
    {
        abilityCurrentlyActive[6] = ability6CurrentlyActive;
        ability6Key.Enable();
        ability6Image.fillAmount = 0;
        ability6Text.text = "";

        ability6RangeIndicator.enabled = false;
        ability6Canvas.enabled = false;
    }

    void Update()
    {
        Ability6Input();
        AbilityCooldown(ref currentAbility6Cooldown, ability6Cooldown, ref isAbility6Cooldown, ability6Image, ability6Text);
    }

    private void Ability6Input()
    {
        if (ability6Key.IsPressed())
        {
            ability6RangeIndicator.enabled = true;
            ability6Canvas.enabled = true;
            Ability6CanvasOn();
        }
        if (ability6Key.WasReleasedThisFrame() && !isAbility6Cooldown)
        {
            ability6RangeIndicator.enabled = false;
            ability6Canvas.enabled = false;
        }
    }

    private void Ability6CanvasOn()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        int layerMask = ~LayerMask.GetMask("player");

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject != this.gameObject)
            {
                position = hit.point;
            }
        }

        var hitPosDir = (hit.point - transform.position).normalized;
        float distance = Vector3.Distance(hit.point, transform.position);
        distance = Mathf.Min(distance, maxAbility6Distance);

        var newHitPos = transform.position + hitPosDir * distance;
        ability6Canvas.transform.position = newHitPos;
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
        ability6Key.Disable();
    }
}
