using UnityEngine;

public class Wizard : Enemy
{
    protected override void Awake()
    {
        base.Awake();

        blackboard.moveSpeed = Stats.MovementSpeed;
        blackboard.attackInterval = 1 / Stats.AttackSpeed;
    }

    protected override void Update()
    {
        ChasePlayer();

        WizardBlackboard blackboard = (WizardBlackboard)base.blackboard;

        if (!blackboard.isPhaseTwo && currentHealth <= MaxHealth * 0.5f)
        {
            blackboard.isPhaseTwo = true;
        }

        base.Update();
    }

    protected override FSM InitializeFSM()
    {
        return new WizardFSM(blackboard);
    }
}
