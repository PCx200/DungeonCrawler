using UnityEngine;

public class IdleState : State
{
    public IdleState(Blackboard blackboard)
    {
        this.blackboard = blackboard;
    }

    public override void Step()
    {
    }
}
