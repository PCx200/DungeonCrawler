using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] NavMeshAgent agent;

    [Header("Player Attributes")]
    #region Stats
    [SerializeField] BaseStatsData baseStatsData;

    public BaseStatsData Stats => baseStatsData;

    float currentHealth;
    float currentMana;
    float currentMovementSpeed;
    float currentDefense;

    [SerializeField] Image HPBar;
    #endregion

    private void Awake()
    {
        currentHealth = Stats.Health;
        currentMana = Stats.Mana;
        currentMovementSpeed = Stats.MovementSpeed;
        currentDefense = Stats.Defense;
    }

    

    void Update()
    {
        Move();
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

        HPBar.fillAmount = currentHealth / baseStatsData.Health;

        if (currentHealth <= 0)
        {
            Debug.Log("Player died!");

            EventBus.OnPlayerDeath.Publish(new PlayerDeathEvent());
        }
    }

    
}
