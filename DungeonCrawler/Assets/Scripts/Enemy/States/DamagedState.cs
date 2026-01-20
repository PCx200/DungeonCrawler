using UnityEngine;
using System.Collections;
using System.Threading;

public class DamagedState: State
{
    float damagedTimer;

    public DamagedState(Blackboard blackboard)
    {
        this.blackboard = blackboard;
    }

    public override void Enter()
    {
        blackboard.animator.SetBool("isDamaged", true);
    }

    public override void Step()
    {
        damagedTimer += Time.deltaTime;
        if (damagedTimer > 0.3)
        {
            blackboard.isDamaged = false;
            damagedTimer = 0;
        }
    }

    public override void Exit()
    {
        blackboard.animator.SetBool("isDamaged", false);
    }
}
