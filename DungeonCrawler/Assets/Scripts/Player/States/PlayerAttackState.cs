using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttackState : State
{
    float timer;
    bool attackPerformed;

    public PlayerAttackState(Blackboard bb) { blackboard = bb; }

    public override void Enter()
    {
        timer = 0f;
        attackPerformed = false;
        blackboard.animator.SetBool("isAttacking", true);
    }

    public override void Step()
    {
        timer += Time.deltaTime;

        if (!attackPerformed && timer >= blackboard.attackInterval)
        {
            attackPerformed = true;
            //blackboard.stateOwnerTransform.GetComponent<Player>().Attack();
            timer = 0f;
        }

        if (attackPerformed && timer >= 0.1f)
        {
            ((PlayerBlackboard)blackboard).isAttacking = false;
            attackPerformed = false;
            timer = 0f;
        }
    }

    public override void Exit()
    {
        blackboard.animator.SetBool("isAttacking", false);
    }
}

