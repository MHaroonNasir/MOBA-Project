using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityCast : CombatSystem
{
    public InputAction ability4Key;
    public InputAction ability5Key;

    Ability4 ability4;
    Ability5 ability5;
    public static int previousId = 0;

    public PlayerAbilityCast abilityCast;
    bool[] abilityCurrentlyActive = PlayerMove.abilityCurrentlyActive;

    private void Awake() {
        base.Awake();
    }

    void Start()
    {
        ability4Key.Enable();
        ability5Key.Enable();
        ability4 = GetComponent<Ability4>();
        ability5 = GetComponent<Ability5>();
    }

    void Update()
    {
        HandlePlayerInput();
        HandleAbilityCastCanvas();
    }

    void HandlePlayerInput()
    {
        if (ability4Key.IsPressed())
        {
            stats.ID.events.testAction?.Invoke();
            abilityCast.Invoke(4, true);
        }
        if (ability5Key.IsPressed())
        {
            stats.ID.sweepAttack?.Invoke();
            abilityCast.Invoke(5, true);
        }
    }

    void HandleAbilityCastCanvas()
    {
        if (ability4Key.IsPressed() && previousId == 4 && !Ability4.isAbility4Cooldown)
        {
            ability5.Ability5DisableCanvas();

            ability4.Ability4CastCanvas();
        }
        else if (ability5Key.IsPressed() && previousId == 5 && !Ability5.isAbility5Cooldown)
        {
            ability4.Ability4DisableCanvas();

            ability5.Ability5CastCanvas();
        }

        if (ability4Key.WasReleasedThisFrame() && !Ability4.isAbility4Cooldown)
        {
            ability4.Ability4Casted();
        }
        else if (ability5Key.WasReleasedThisFrame() && !Ability5.isAbility5Cooldown)
        {
            ability5.Ability5Casted();
        }
    }

    private void DisableAbilityKeys()
    {
        ability4Key.Disable();
        ability5Key.Disable();
    }

    public void toggleAbilityCurrentlyActive(int abilityId, bool currentlyActive)
    {
        abilityCurrentlyActive[abilityId] = currentlyActive;
        if (abilityId != previousId) 
        { 
            previousId = abilityId; 
            //Debug.Log(previousId);
        };
    }
}
