using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class KillEnemyQuest : Quest
{
    [SerializeField] Enemy enemyToKill;

    int currentKilled;

    private void OnEnable()
    {
        EventBus.OnEnemyDieEvent.Subscribe(OnEnemyKilled);
    }

    private void OnDisable()
    {
        EventBus.OnEnemyDieEvent.Unsubscribe(OnEnemyKilled);
    }

    

    void OnEnemyKilled(EnemyDieEvent e)
    {
        if (e.Enemy.Stats == enemyToKill.Stats)
        {
            UpdateQuest();
        }
    }

    public override void UpdateQuest()
    {
        currentKilled++;
        if (currentKilled == amountToComplete)
        {
            Debug.Log("Quest Completed");
        }
        else
        {
            Debug.Log(currentKilled);
        }
    }
}
