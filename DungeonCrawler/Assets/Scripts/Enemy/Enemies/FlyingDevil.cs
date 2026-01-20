using UnityEngine;

public class FlyingDevil : Enemy
{
    protected override void Awake()
    {
        base.Awake();

        blackboard.moveSpeed = Stats.MovementSpeed;
        blackboard.attackInterval = 1 / Stats.AttackSpeed;
    }
    protected override void Update()
    {
        if (GameObject.FindWithTag("Player") != null)
        {
            blackboard.targetPosition = GameObject.FindWithTag("Player").transform.position;
        }
        base.Update();
    }

    public override void Attack()
    {
        base.Attack();
    }

    public override void Die()
    {
        base.Die();
    }

    public override void Move()
    {
        base.Move();
    }

    protected override FSM InitializeFSM()
    {
        return new FlyingDevilFSM(blackboard);
    }
}
