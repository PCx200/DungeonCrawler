using UnityEngine;

public class ChaseState : State
{

    public ChaseState(Blackboard blackboard)
    {
        this.blackboard = blackboard;
    }
    public override void Step()
    {
        blackboard.stateOwnerTransform.GetComponent<Enemy>().Move();
    }
    public override void Enter()
    {
        blackboard.animator.SetBool("isChasing", true);
    }
    public override void Exit() 
    { 
        blackboard.animator.SetBool("isChasing", false); 
    }
}
