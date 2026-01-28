using UnityEngine;

public class PlayerMoveState : State
{
    public PlayerMoveState(Blackboard bb) { blackboard = bb; }

    public override void Enter()
    {
        blackboard.animator.SetBool("isMoving", true);
        blackboard.agent.isStopped = false;
    }

    public override void Step()
    {
        blackboard.agent.speed = blackboard.moveSpeed;
    }

    public override void Exit()
    {
        blackboard.animator.SetBool("isMoving", false);
    }
}

