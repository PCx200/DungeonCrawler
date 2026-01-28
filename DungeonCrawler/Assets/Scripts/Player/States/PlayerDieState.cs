using UnityEngine;

public class PlayerDieState : State
{
    public PlayerDieState(Blackboard bb) { blackboard = bb; }

    public override void Enter()
    {
        blackboard.animator.SetBool("isDead", true);
    }

    public override void Step()
    {
        blackboard.stateOwnerTransform.GetComponent<Player>().Die();
    }
}

