using UnityEngine;
using System.Collections.Generic;

public class PlayerCombat : MonoBehaviour
{
    [Header("Data")]
    public int characterId;
    public string characterName;

    [Header("Current Ability State")]
    public AbilityData currentAbility;
    public ComboStep CurrentComboStep { get; private set; }

    private Animator animator;
    private InputManager inputManager;
    private int comboIndex = 0;
    private float lastAttackTime = 0f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        inputManager = GetComponent<InputManager>();
    }

    public void LoadAbility(AbilityData ability)
    {
        currentAbility = ability;
        comboIndex = 0;
    }

    private void Update()
    {
        if (inputManager.IsAttackPressed())
        {
            TryExecuteCombo();
            AbilityExecutor.ExecuteActionAtTiming("OnHit1", gameObject);
        }
    }

    public void TryExecuteCombo()
    {
        if (currentAbility == null || Time.time - lastAttackTime < currentAbility.comboSteps[comboIndex].cooldown)
        {
            Debug.Log("[PlayerCombat] No hay una Abilidad seleccionada en el personaje o la habilidad está en Cooldown.");
            return;
        }

        CurrentComboStep = currentAbility.comboSteps[comboIndex];
        AbilityExecutor.ExecuteComboStep(CurrentComboStep, gameObject);

        animator.Play(CurrentComboStep.animation);
        lastAttackTime = Time.time;

        comboIndex = (comboIndex + 1) % currentAbility.comboSteps.Count;
    }

    // Reset combo manually if needed (e.g. after timeout)
    public void ResetCombo()
    {
        comboIndex = 0;
        CurrentComboStep = null;
    }
}
