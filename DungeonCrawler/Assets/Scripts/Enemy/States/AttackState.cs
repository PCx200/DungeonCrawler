using UnityEngine;

public class AttackState : State
{
    private float timer;
    public AttackState(Blackboard blackboard)
    {
        this.blackboard = blackboard;
    }

    public override void Enter()
    {
        timer = 0f;
        blackboard.animator.SetBool("isAttacking", true);
    }
    public override void Exit()
    {
        blackboard.animator.SetBool("isAttacking", false);
    }

    public override void Step()
    {
        timer += Time.deltaTime; 
        if (timer >= blackboard.attackInterval)
        {
            blackboard.stateOwnerTransform.GetComponent<Enemy>().Attack();
            timer = 0f; 
        }
    }
}
