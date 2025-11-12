using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] string playerName;
    public string Name => playerName;

    [SerializeField] NavMeshAgent agent;

    [Header("Player Attributes")]
    #region Stats
    [SerializeField] BaseStatsData baseStatsData; // Base stats created when you first play the game
    [SerializeField] PlayerProgressData progressData; // The current stats your character has 

    public BaseStatsData Stats => baseStatsData;

    public PlayerProgressData ProgressData => progressData;

    float currentHealth;
    public float CurrentHealth => currentHealth;
    public float MaxHealth => progressData.MaxHealth;

    float currentAttack;
    float currentMana;
    float currentMovementSpeed;
    float currentDefense;

    int currentLvl;
    public int CurrentLevel => progressData.Level;

    [SerializeField] List<Image> HPBars;
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
        currentLvl = progressData.Level;
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

        EventBus.OnPlayerDamaged.Publish(new PlayerDamagedEvent
        {
            CurrentHealth = currentHealth,
            MaxHealth = progressData.MaxHealth
        });

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

        EventBus.OnPlayerDamaged.Publish(new PlayerDamagedEvent
        {
            CurrentHealth = currentHealth,
            MaxHealth = progressData.MaxHealth
        }); 

        if (currentHealth <= 0)
        {
            Debug.Log("Player died!");

            EventBus.OnPlayerDeath.Publish(new PlayerDeathEvent());
        }
    }

    public void LevelUp()
    {
        progressData.Level++;
        progressData.XP -= progressData.XPToNextLevel;
        progressData.XPToNextLevel = Mathf.RoundToInt(progressData.XPToNextLevel * 1.25f);

        Debug.Log($"Level up! New Level: {ProgressData.Level}");

        progressData.MaxHealth += 20;
        progressData.MaxAttack += 5;

        currentHealth = progressData.MaxHealth;
        currentLvl = progressData.Level;

        EventBus.OnLevelUp.Publish(new LevelUpEvent { NewLevel = progressData.Level });

        Debug.Log($"{ currentHealth}, { progressData.MaxHealth }");

        Debug.Log($"Player leveled up to {currentLvl}! Stats increased: +{20} HP, +{5} ATK.");
    }

    public void ResetStats()
    {
        progressData.ResetStats(baseStatsData);

        InitializeBaseStats();

        Debug.Log("Player stats reseted!");
    }
}
