using UnityEngine;

public class Goblin : Enemy
{



    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

    public override void Die()
    {
        EventBus.OnEnemyDieEvent.Publish(new EnemyDieEvent()
        {
            Enemy = this,
            Position = transform.position,
            XPGivenAmount = XPAmount
        });

        Destroy(gameObject);
    }

    public override void Move()
    {
        throw new System.NotImplementedException();
    }
}
