using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] string playerName;
    public string Name => playerName;

    [SerializeField] NavMeshAgent agent;

    [Header("Stats")]
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

    float attackRange;

    int currentLvl;
    public int CurrentLevel => progressData.Level;
    public float CurrentAttack => progressData.MaxAttack;

    [SerializeField] List<Image> HPBars;
    #endregion

    [Header("Attack Attributes")]
    [SerializeField] SphereCollider attackCollider;
    [SerializeField] SpriteRenderer attackRangeSprite;

    [SerializeField] Bullet bulletPrefab;
    [SerializeField] Transform projectileSpawnPoint;

    [SerializeField] float lastAttackTime;

    [Header("Inventory")]
    [SerializeField] Inventory inventory;


    private void Awake()
    {
        InitializeBaseStats();
    }

    void Start()
    {
        agent.speed = currentMovementSpeed;
        attackCollider.radius = attackRange;
        attackRangeSprite.transform.localScale = new Vector2(attackRange * 2, attackRange * 2);
    }

    void Update()
    {
        Move();
        //ShowRange();
        Attack();
        KillAllEnemies();
        Heal();
    }

    private void InitializeBaseStats()
    {
        currentHealth = progressData.MaxHealth;
        currentAttack = progressData.MaxAttack;
        currentMana = progressData.MaxMana;
        currentMovementSpeed = progressData.MaxMovementSpeed;
        currentDefense = progressData.MaxDefense;
        currentLvl = progressData.Level;


        attackRange = baseStatsData.AttackRange;
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

    void Attack()
    {
        if ((Mouse.current.leftButton.wasPressedThisFrame || Mouse.current.leftButton.isPressed) && Time.time - lastAttackTime >= 1 / baseStatsData.AttackSpeed)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (!Physics.Raycast(ray, out RaycastHit hit))
                return;

            Vector3 targetPoint = hit.point;

            Vector3 direction = (targetPoint - projectileSpawnPoint.position).normalized;

            DamageData dmg = new DamageData(
                currentAttack,
                DamageType.Physical,
                0, 0,
                gameObject
            );

            Bullet bulletObj = Instantiate(bulletPrefab, projectileSpawnPoint.position, Quaternion.LookRotation(direction));

            bulletObj.GetComponent<Bullet>().Initialize(dmg);

            lastAttackTime = Time.time;
        }   
    }

    void ShowRange()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame || Mouse.current.leftButton.isPressed)
        {
            attackRangeSprite.gameObject.SetActive(true);
        }
        else
        {
            attackRangeSprite.gameObject.SetActive(false);
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
            Die();
        }
    }

    // For testing purposes only
    public void TakeDMG()
    {
        float damageTaken = (float)Math.Round(10 * (100f / (100f + currentDefense)), 1);
        Debug.Log(damageTaken);
        currentHealth -= damageTaken;

        EventBus.OnPlayerDamaged.Publish(new PlayerDamagedEvent
        {
            CurrentHealth = currentHealth,
            MaxHealth = progressData.MaxHealth
        }); 

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Debug.Log("Player died!");
        EventBus.OnPlayerDeath.Publish(new PlayerDeathEvent());
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

        EventBus.OnStatsReset.Publish(new PlayerStatsResetEvent { });

        Debug.Log("Player stats reseted!");
    }

    public void KillAllEnemies()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Enemy[] allEnemies = FindObjectsByType<Enemy>(sortMode: FindObjectsSortMode.None);


            foreach (Enemy enemy in allEnemies)
            {
                //if (Vector3.Distance(transform.position, enemy.transform.position) < attackRange)
                //{
                    enemy.Die();
                //}
            }
        }

    }

    void Heal()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame && currentHealth < MaxHealth && inventory.GetSlot(0).Amount > 0)
        {
            //inventory.RemoveItem(inventory.GetSlot(0).item);
            currentHealth += 10;
            EventBus.OnPlayerHealed.Publish(new PlayerHealedEvent { HealAmount = 10, CurrentHealth = currentHealth });
        }
    }
}
