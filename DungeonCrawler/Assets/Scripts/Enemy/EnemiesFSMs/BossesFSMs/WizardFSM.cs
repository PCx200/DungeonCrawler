using UnityEngine;

public class WizardFSM : FSM
{
    public WizardFSM(Blackboard blackboard)
    {
        this.blackboard = blackboard;

        IdleState idle = new IdleState(blackboard);
        ChaseState chase = new ChaseState(blackboard);
        AttackState attack = new AttackState(blackboard);
        DamagedState damaged = new DamagedState(blackboard);
        DieState die = new DieState(blackboard);

        SummonState summon = new SummonState(blackboard);

        idle.transitions.Add(new Transition(() => DistanceToPlayer() < 10f, chase));
        chase.transitions.Add(new Transition(() => DistanceToPlayer() < 2f, attack));
        attack.transitions.Add(new Transition(() => DistanceToPlayer() > 2.5f && attack.IsAttackPerformed, chase));

        idle.transitions.Add(new Transition(() => IsPhaseTwo(), summon));
        chase.transitions.Add(new Transition(() => IsPhaseTwo(), summon));
        attack.transitions.Add(new Transition(() => IsPhaseTwo(), summon));

        summon.transitions.Add(new Transition(() => summon.IsDoneSummoning, chase));

        idle.transitions.Add(new Transition(() => blackboard.isDamaged, damaged));
        chase.transitions.Add(new Transition(() => blackboard.isDamaged, damaged));
        attack.transitions.Add(new Transition(() => blackboard.isDamaged, damaged));
        summon.transitions.Add(new Transition(() => blackboard.isDamaged, damaged));

        damaged.transitions.Add(new Transition(() => !blackboard.isDamaged && DistanceToPlayer() > 10f, idle));
        damaged.transitions.Add(new Transition(() => !blackboard.isDamaged && DistanceToPlayer() < 10f && !IsPhaseTwo(), chase));
        damaged.transitions.Add(new Transition(() => !blackboard.isDamaged && IsPhaseTwo(), summon));

        idle.transitions.Add(new Transition(() => blackboard.isDead, die));
        chase.transitions.Add(new Transition(() => blackboard.isDead, die));
        attack.transitions.Add(new Transition(() => blackboard.isDead, die));
        summon.transitions.Add(new Transition(() => blackboard.isDead, die));

        currentState = idle;
        currentState.Enter();
    }

    float DistanceToPlayer()
    {
        return Vector3.Distance(
            blackboard.stateOwnerTransform.position,
            blackboard.targetPosition
        );
    }

    bool IsPhaseTwo()
    {
        return ((WizardBlackboard)blackboard).isPhaseTwo;
    }
}
