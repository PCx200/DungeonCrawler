using System;
using UnityEngine;
    using UnityEngine.Scripting;

    public abstract class Enemy : MonoBehaviour, IDamageable
    {
        [SerializeField] string enemyName;

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
            if (Stats == null || ItemDropTable == null)
            {
                Debug.LogError($"{name}: Missing required references!", this);
                enabled = false;
                return;
            }

            currentHealth = Stats.Health;
            currentMana = Stats.Mana;
            currentMovementSpeed = Stats.MovementSpeed;
            currentDefense = Stats.Defense;
        }

        public abstract void Move();
        public abstract void Attack();
        public abstract void Die();

        public void TakeDamage(DamageData damageData)
        {
            float damageTaken = damageData.damage * (100f / (100f + currentDefense));

            currentHealth -= damageTaken;

            Debug.Log($"Enemy: {this} took {damageTaken} damage.");

            EventBus.OnEnemyDamaged.Publish(new EnemyDamagedEvent { Enemy = this });

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
    }
