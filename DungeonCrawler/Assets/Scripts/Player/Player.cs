using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] NavMeshAgent agent;

    [Header("Player Attributes")]
    #region Stats
    [SerializeField] BaseStatsData baseStatsData; // Base stats created when you first play the game
    [SerializeField] PlayerProgressData progressData; // The current stats your character has 

    public BaseStatsData Stats => baseStatsData;

    float currentHealth;
    float currentAttack;
    float currentMana;
    float currentMovementSpeed;
    float currentDefense;

    [SerializeField] Image HPBar;
    #endregion

    private void Awake()
    {
        InitializeBaseStats();
    }
    void Update()
    {
        Move();
    }

    private void InitializeBaseStats()
    {
        currentHealth = progressData.MaxHealth;
        currentAttack = progressData.MaxAttack;
        currentMana = progressData.MaxMana;
        currentMovementSpeed = progressData.MaxMovementSpeed;
        currentDefense = progressData.MaxDefense;
    }

    void Move()
    {
        if (Mouse.current.rightButton.wasPressedThisFrame || Mouse.current.rightButton.isPressed)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }
    }

    public void TakeDamage(DamageData damageData)
    {
        float damageTaken = damageData.damage * (100f / (100f + currentDefense));

        currentHealth -= damageTaken;

        if (currentHealth <= 0)
        {
            Debug.Log("Player died!");

            EventBus.OnPlayerDeath.Publish(new PlayerDeathEvent());
        }
    }

    // For testing purposes only
    public void TakeDMG()
    {
        float damageTaken = 10 * (100f / (100f + currentDefense));
        Debug.Log(damageTaken);
        currentHealth -= damageTaken;

        HPBar.fillAmount = currentHealth / progressData.MaxHealth;

        if (currentHealth <= 0)
        {
            Debug.Log("Player died!");

            EventBus.OnPlayerDeath.Publish(new PlayerDeathEvent());
        }
    }

    private void OnLevelUp(LevelUpEvent e)
    {
        progressData.MaxHealth += 20;
        progressData.MaxAttack += 5;

        HPBar.fillAmount = currentHealth / progressData.MaxHealth;

        Debug.Log($"{ currentHealth}, { progressData.MaxHealth }");

        Debug.Log($"Player leveled up to {e.NewLevel}! Stats increased: +{20} HP, +{5} ATK.");
    }

    private void OnStatsReset(PlayerStatsResetEvent e)
    {
        progressData.ResetStats(baseStatsData);

        InitializeBaseStats();

        HPBar.fillAmount = currentHealth / progressData.MaxHealth;

        EventBus.OnStatsReset.Publish(new PlayerStatsResetEvent());

        Debug.Log("Player stats reseted!");
    }

    public void ResetStats()
    {
        OnStatsReset(new PlayerStatsResetEvent { });
    }
    private void OnEnable()
    {
        EventBus.OnLevelUp.Subscribe(OnLevelUp);
        EventBus.OnStatsReset.Subscribe(OnStatsReset);
    }

    private void OnDisable()
    {
        EventBus.OnLevelUp.Unsubscribe(OnLevelUp);
        EventBus.OnStatsReset.Unsubscribe(OnStatsReset);
    }
}
