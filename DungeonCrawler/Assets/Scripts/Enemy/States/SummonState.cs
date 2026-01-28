using UnityEngine;

public class SummonState : State
{
    public bool IsDoneSummoning { get; private set; }

    float timer;

    public SummonState(Blackboard blackboard)
    {
        this.blackboard = blackboard;
    }

    public override void Enter()
    {
        IsDoneSummoning = false;
        timer = 0f;

        blackboard.animator.SetBool("isSummoning", true);
        blackboard.agent.isStopped = true;
    }

    public override void Step()
    {
        WizardBlackboard wizBB = (WizardBlackboard)blackboard;

        timer += Time.deltaTime;

        if (timer >= wizBB.summonInterval)
        {
            timer = 0f;
            SpawnSpawners(wizBB);
            IsDoneSummoning = true;
        }
    }

    void SpawnSpawners(WizardBlackboard wizBB)
    {
        for (int i = 0; i < wizBB.spawnersPerWave; i++)
        {
            Vector3 offset = new Vector3(
                Random.Range(-4f, 4f),
                0,
                Random.Range(-4f, 4f)
            );

            Object.Instantiate(
                wizBB.spawnerPrefab,
                blackboard.stateOwnerTransform.position + offset,
                Quaternion.identity
            );
        }
    }

    public override void Exit()
    {
        blackboard.animator.SetBool("isSummoning", false);
        blackboard.agent.isStopped = false;
    }
}
