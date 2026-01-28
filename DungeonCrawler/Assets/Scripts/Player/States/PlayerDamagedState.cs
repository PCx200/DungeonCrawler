using UnityEngine;

public class PlayerDamagedState : State
{
    float timer;

    public PlayerDamagedState(Blackboard bb) { blackboard = bb; }

    public override void Enter()
    {
        blackboard.animator.SetBool("isDamaged", true);
    }

    public override void Step()
    {
        timer += Time.deltaTime;
        if (timer > 0.1f)
        {
            blackboard.isDamaged = false;
            timer = 0f;
        }
    }

    public override void Exit()
    {
        blackboard.animator.SetBool("isDamaged", false);
    }
}

