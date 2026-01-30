using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Scripting;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] string enemyName;

    [SerializeField] protected Blackboard blackboard;
    protected FSM fsm;
    
    [Header("Base Stats and Items to Drop")]
    [SerializeField] BaseStatsData baseStatsData;
    [SerializeField] ItemDropTable itemDropTable;

    public BaseStatsData Stats => baseStatsData;
    public ItemDropTable ItemDropTable => itemDropTable;

    protected float currentHealth;
    public float CurrentHealth => currentHealth;
    public float MaxHealth => Stats.Health;

    protected float currentMana;
    protected float currentMovementSpeed;
    protected float currentDefense;

    [SerializeField] int xpAmount;
    public int XPAmount => xpAmount;

    protected virtual void Awake()
    {
        blackboard = GetComponent<Blackboard>();

        fsm = InitializeFSM();
        fsm.Enter();

        if (Stats == null || ItemDropTable == null)
        {
            Debug.LogError($"Missing required references!", this);
            return;
        }

        InitializeStats();
    }

    private void InitializeStats()
    {
        currentHealth = Stats.Health;
        currentMana = Stats.Mana;
        currentMovementSpeed = Stats.MovementSpeed;
        currentDefense = Stats.Defense;

        blackboard.agent.speed = Stats.MovementSpeed;
    }

    protected abstract FSM InitializeFSM();


    protected virtual void Update() 
    { 
        fsm.Step(); 
    }

    public virtual void Move()
    {
        if (blackboard.isDead) return;

        blackboard.agent.isStopped = false;
        blackboard.agent.speed = Stats.MovementSpeed;
        blackboard.agent.SetDestination(blackboard.targetPosition);
    }
    public virtual void Attack() 
    {
        blackboard.agent.speed = 0f;
        blackboard.agent.isStopped = true;

        Player player = GameObject.FindWithTag("Player").GetComponent<Player>();

        if (player != null)
        {
            DamageData damage = new DamageData(Stats.Attack);

            player.TakeDamage(damage);
        }
    }
    public virtual void Die() 
    {
        if (blackboard.isDead) return;

        blackboard.agent.speed = 0f;
        blackboard.agent.isStopped = true;
        blackboard.isDead = true;


        EventBus.OnEnemyDieEvent.Publish(new EnemyDieEvent()
        {
            Enemy = this,
            Position = transform.position,
            XPGivenAmount = XPAmount
        });

        Destroy(gameObject, 3.5f);
    }

    public void TakeDamage(DamageData damageData)
    {
        float damageTaken = damageData.damage * (100f / (100f + currentDefense));

        currentHealth -= damageTaken;

        Debug.Log($"Enemy: {this} took {damageTaken} damage.");

        EventBus.OnEnemyDamaged.Publish(new EnemyDamagedEvent { Enemy = this });

        blackboard.isDamaged = true;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void TakeDMG()
    {
        DamageData dmg = new DamageData(10) { damage = 10 };
        TakeDamage(dmg);
    }

    protected void ChasePlayer()
    {
        if (GameObject.FindWithTag("Player") != null)
        {
            blackboard.targetPosition = GameObject.FindWithTag("Player").transform.position;
        }
    }
}
