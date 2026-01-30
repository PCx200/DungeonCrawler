using UnityEngine;

public class WizardBlackboard : Blackboard
{
    [Header("Wizard Summoning Settings")]
    public Spawner spawnerPrefab;       
    public int spawnersPerWave = 1;     
    public float summonInterval = 5f;   
    public bool isPhaseTwo;

    public float summonDuration = 5f;
    public float lastSummonTime; 

}
