using UnityEngine;

public class Goblin : Enemy
{
    protected override void Awake()
    {
        base.Awake();

        blackboard.moveSpeed = Stats.MovementSpeed;
    }

    protected override void Update()
    {
        if (GameObject.FindWithTag("Player") != null) { 
            blackboard.targetPosition = GameObject.FindWithTag("Player").transform.position; 
        }
        base.Update();
    }
    protected override FSM InitializeFSM()
    {
        return new GoblinFSM(blackboard);
    }
    public override void Attack()
    {
        blackboard.agent.speed = 0f;
        blackboard.agent.isStopped = true;
    }

    public override void Die()
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

    public override void Move()
    {
        if (blackboard.isDead) return;

        blackboard.agent.isStopped = false;
        blackboard.agent.speed = Stats.MovementSpeed;
        blackboard.agent.SetDestination(blackboard.targetPosition);
    }

}
