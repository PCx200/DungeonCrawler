using UnityEngine;

public class Wizard : Enemy
{
    [Header("Wizard Projectile Settings")]
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;

    protected override void Awake()
    {
        base.Awake();

        blackboard.moveSpeed = Stats.MovementSpeed;
        blackboard.attackInterval = 1f / Stats.AttackSpeed;
    }

    protected override void Update()
    {
        ChasePlayer();

        WizardBlackboard wizardBlackboard = (WizardBlackboard)blackboard;

        if (!wizardBlackboard.isPhaseTwo && currentHealth <= MaxHealth * 0.5f)
        {
            wizardBlackboard.isPhaseTwo = true;
        }

        base.Update();
    }

    protected override FSM InitializeFSM()
    {
        return new WizardFSM(blackboard);
    }

    public override void Attack()
    {
        blackboard.agent.speed = 0f;
        blackboard.agent.isStopped = true;

        GameObject playerGO = GameObject.FindWithTag("Player");
        if (playerGO == null)
        {
            return;
        }

        Vector3 spawnPos = projectileSpawnPoint != null ? projectileSpawnPoint.position : transform.position;
        Vector3 dir = (playerGO.transform.position - spawnPos).normalized;
        if (dir == Vector3.zero) dir = transform.forward;

        Quaternion rot = Quaternion.LookRotation(dir);

        if (projectilePrefab != null)
        {
            GameObject projGO = Instantiate(projectilePrefab, spawnPos, rot);
            Projectile proj = projGO.GetComponent<Projectile>();
            if (proj != null)
            {
                proj.Initialize(new DamageData(Stats.Attack));
            }
        }
    }
}
