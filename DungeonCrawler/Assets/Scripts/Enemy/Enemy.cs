using UnityEngine;
using UnityEngine.Scripting;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] string enemyName;

    [Header("Base Stats and Items to Drop")]
    [SerializeField] BaseStatsData baseStatsData;
    [SerializeField] ItemDropTable itemDropTable;

    public BaseStatsData BaseStatsData => baseStatsData;
    public ItemDropTable ItemDropTable => itemDropTable;

    protected float currentHealth;
    protected float currentMana;
    protected float currentMovementSpeed;
    protected float currentDefense;

    protected virtual void Awake()
    {
        if (BaseStatsData == null || ItemDropTable == null)
        {
            Debug.LogError($"{name}: Missing required references!", this);
            enabled = false;
            return;
        }

        currentHealth = BaseStatsData.Health;
        currentMana = BaseStatsData.Mana;
        currentMovementSpeed = BaseStatsData.MovementSpeed;
        currentDefense = BaseStatsData.Defense;
    }

    public abstract void Move();
    public abstract void Attack();
    public abstract void Die();

    public virtual void TakeDamage(float damage)
    {
        float finalDamage = Mathf.Max(1, damage - currentDefense);
        currentHealth -= finalDamage;

        if (currentHealth <= 0)
            Die();
    }

}
