using UnityEngine;

public class FlyingDevilFSM: FSM
{
    public FlyingDevilFSM(Blackboard blackboard)
    {
        this.blackboard = blackboard;

        IdleState idle = new IdleState(blackboard);
        ChaseState chase = new ChaseState(blackboard);
        AttackState attack = new AttackState(blackboard);
        DieState die = new DieState(blackboard);
        DamagedState damaged = new DamagedState(blackboard);

        idle.transitions.Add(new Transition(() => Vector3.Distance(blackboard.stateOwnerTransform.position, blackboard.targetPosition) < 12f, chase));

        chase.transitions.Add(new Transition(() => Vector3.Distance(blackboard.stateOwnerTransform.position, blackboard.targetPosition) > 12.5f, idle));

        chase.transitions.Add(new Transition(() => Vector3.Distance(blackboard.stateOwnerTransform.position, blackboard.targetPosition) < 1.5f, attack));

        attack.transitions.Add(new Transition(() =>
        Vector3.Distance(blackboard.stateOwnerTransform.position, blackboard.targetPosition) > 1.5f
        && attack.IsAttackPerformed,
        chase));

        idle.transitions.Add(new Transition(() => blackboard.isDamaged, damaged));
        chase.transitions.Add(new Transition(() => blackboard.isDamaged, damaged));
        attack.transitions.Add(new Transition(() => blackboard.isDamaged, damaged));

        damaged.transitions.Add(new Transition(() => !blackboard.isDamaged && Vector3.Distance(blackboard.stateOwnerTransform.position, blackboard.targetPosition) > 12.5f, idle));
        damaged.transitions.Add(new Transition(() => !blackboard.isDamaged && Vector3.Distance(blackboard.stateOwnerTransform.position, blackboard.targetPosition) < 12f, chase));
        damaged.transitions.Add(new Transition(() => !blackboard.isDamaged && Vector3.Distance(blackboard.stateOwnerTransform.position, blackboard.targetPosition) < 1.5f, attack));

        damaged.transitions.Add(new Transition(() => blackboard.isDead, die));

        currentState = idle;
        currentState.Enter();
    }
}
