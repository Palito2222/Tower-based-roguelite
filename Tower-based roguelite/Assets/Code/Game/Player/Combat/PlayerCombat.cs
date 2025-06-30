using UnityEngine;

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
        animator = GetComponentInChildren<Animator>();
        inputManager = GetComponent<InputManager>();
        ConfigManager.ClearCache();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private async void Start()
    {
        string characterName = "Kaoru";
        string abilityScript = "Kaoru_BasicSkill";

        string path = $"Assets/ArrozResources/Data/Abilities/CharacterAbility/{characterName}/{abilityScript}.json";
        currentAbility = await ConfigManager.LoadAsync<AbilityData>(path);
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
        }
    }

    public void TryExecuteCombo()
    {
        if (currentAbility == null || Time.time - lastAttackTime < currentAbility.comboSteps[comboIndex].cooldown)
        {
            Debug.Log("[PlayerCombat] No hay una Abilidad seleccionada en el personaje o la habilidad está en Cooldown.");
            return;
        }

        if (comboIndex >= currentAbility.comboSteps.Count)
        {
            comboIndex = 0;
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
