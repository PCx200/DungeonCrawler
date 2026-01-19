using Unity.VisualScripting;
using UnityEngine;

public class Breakable : MonoBehaviour, IDamageable
{
    [SerializeField] BaseStatsData stats;
    [SerializeField] ItemDropTable dropTable;

    public void TakeDamage(DamageData damageData)
    {
        EventBus.OnEnemyDieEvent.Publish(new EnemyDieEvent() { Enemy = null, ItemDropTable = dropTable, Position = transform.position} );
        Destroy(gameObject);
    }
}
