using UnityEngine;

public class PlayerFSM : FSM
{
    public PlayerFSM(Blackboard blackboard)
    {
        this.blackboard = blackboard;

        IdleState idle = new IdleState(blackboard);
        PlayerMoveState move = new PlayerMoveState(blackboard);
        PlayerAttackState attack = new PlayerAttackState(blackboard);
        PlayerDamagedState damaged = new PlayerDamagedState(blackboard);
        PlayerDieState die = new PlayerDieState(blackboard);

        idle.transitions.Add(new Transition(() => ((PlayerBlackboard)blackboard).isAttacking, attack));
        attack.transitions.Add(new Transition(() => !((PlayerBlackboard)blackboard).isAttacking, move));

        idle.transitions.Add(new Transition(() =>
            blackboard.agent.velocity.magnitude > 1f, move));

        move.transitions.Add(new Transition(() =>
            blackboard.agent.velocity.magnitude < 1f && !((PlayerBlackboard)blackboard).isAttacking, idle));

        move.transitions.Add(new Transition(() =>
            ((PlayerBlackboard)blackboard).isAttacking, attack));

        idle.transitions.Add(new Transition(() => blackboard.isDamaged, damaged)); 
        move.transitions.Add(new Transition(() => blackboard.isDamaged, damaged)); 
        attack.transitions.Add(new Transition(() => blackboard.isDamaged, damaged)); 
        damaged.transitions.Add(new Transition(() => !blackboard.isDamaged, idle));

        idle.transitions.Add(new Transition(() => blackboard.isDead, die));
        move.transitions.Add(new Transition(() => blackboard.isDead, die));
        attack.transitions.Add(new Transition(() => blackboard.isDead, die));
        damaged.transitions.Add(new Transition(() => blackboard.isDead, die));
        currentState = idle;
    }
}

