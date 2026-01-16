using UnityEngine;

public class DieState : State
{
    public DieState(Blackboard blackboard)
    {
        this.blackboard = blackboard;
    }

    public override void Step()
    {
        blackboard.stateOwnerTransform.GetComponent<Enemy>().Die();
    }
    public override void Enter()
    {
        blackboard.animator.SetBool("isDead", true);
    }

}
