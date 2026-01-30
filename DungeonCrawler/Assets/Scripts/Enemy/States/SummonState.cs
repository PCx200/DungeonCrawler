using UnityEngine;

public class SummonState : State
{
    public bool IsDoneSummoning { get; private set; }

    float summonDurationTimer;

    WizardBlackboard wizardBlackboard;


    public SummonState(Blackboard blackboard)
    {
        this.blackboard = blackboard;
        wizardBlackboard = (WizardBlackboard)this.blackboard;
    }

    public override void Enter()
    {
        IsDoneSummoning = false;
        summonDurationTimer = 0f;

        blackboard.animator.SetBool("isSummoning", true);
        blackboard.agent.isStopped = true;

        wizardBlackboard.lastSummonTime = Time.time;

    }

    public override void Step()
    {
        summonDurationTimer += Time.deltaTime;

        if (summonDurationTimer >= 2f)
        {
            SpawnSpawners();
        }
        if (summonDurationTimer >= wizardBlackboard.summonDuration)
        { 
            IsDoneSummoning = true; 
        }

    }

    void SpawnSpawners()
    {
        for (int i = 0; i < wizardBlackboard.spawnersPerWave; i++)
        {
            Vector3 offset = new Vector3(
                Random.Range(-4f, 4f),
                0,
                Random.Range(-4f, 4f)
            );

            Object.Instantiate(
                wizardBlackboard.spawnerPrefab,
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
