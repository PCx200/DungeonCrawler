using UnityEngine;

public class AttackState : State
{
    float timer;
    bool attackPerformed;
    public AttackState(Blackboard blackboard)
    {
        this.blackboard = blackboard;
    }

    public override void Enter()
    {
        timer = 0f;
        attackPerformed = false;
        blackboard.animator.SetBool("isAttacking", true);
    }
    public override void Exit()
    {
        blackboard.animator.SetBool("isAttacking", false);
    }

    public override void Step()
    {
        timer += Time.deltaTime;
        if (!attackPerformed && timer >= blackboard.attackInterval)
        {
            attackPerformed = true;
            blackboard.stateOwnerTransform.GetComponent<Enemy>().Attack();
            timer = 0f;
        }
        if (attackPerformed && timer >= blackboard.attackInterval - (blackboard.attackInterval - 0.1f))
        {
            attackPerformed = false;
            timer = 0f;
        }
    }

    public bool IsAttackPerformed => attackPerformed;
}
