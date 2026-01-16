using UnityEngine;

public class GoblinFSM : FSM
{

    public GoblinFSM(Blackboard blackboard)
    {
        this.blackboard = blackboard;

        IdleState idle = new IdleState(blackboard);
        ChaseState chase = new ChaseState(blackboard);
        AttackState attack = new AttackState(blackboard);
        DieState die = new DieState(blackboard);

        idle.transitions.Add(new Transition(() => Vector3.Distance(blackboard.stateOwnerTransform.position, blackboard.targetPosition) < 7f, chase));

        chase.transitions.Add(new Transition(() => Vector3.Distance(blackboard.stateOwnerTransform.position, blackboard.targetPosition) > 7.5f, idle));

        chase.transitions.Add(new Transition(() => Vector3.Distance(blackboard.stateOwnerTransform.position, blackboard.targetPosition) < 2f, attack));

        attack.transitions.Add(new Transition(() => Vector3.Distance(blackboard.stateOwnerTransform.position, blackboard.targetPosition) > 3f, chase));

        idle.transitions.Add(new Transition(() => blackboard.isDead, die));
        chase.transitions.Add(new Transition(() => blackboard.isDead, die));
        attack.transitions.Add(new Transition(() => blackboard.isDead, die));

        currentState = idle;
        currentState.Enter();
    }
}
