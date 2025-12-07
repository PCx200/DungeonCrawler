using UnityEditor.Experimental.GraphView;
using UnityEditor.Search;
using UnityEngine;

public class DropSystem : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        EventBus.OnEnemyDieEvent.Subscribe(HandleEnemyDie);
    }

    private void OnDisable()
    {
        EventBus.OnEnemyDieEvent.Unsubscribe(HandleEnemyDie);
    }

    private void HandleEnemyDie(EnemyDieEvent e)
    {
        ItemDropTable dropTable = e.Enemy.ItemDropTable;
        Vector3 position = e.Position;

        // Drop money
        if (dropTable.Money != null)
        {
            int amount = dropTable.Money.Amount;
            GameObject moneyInstance = Instantiate(dropTable.Money.Prefab, position, Quaternion.identity);
            moneyInstance.GetComponent<CurrencyPicker>().Initialize(amount);
        }

        // Drop items if lucky 
        foreach (var dropEntry in dropTable.Items)
        {
            if (Random.value <= dropEntry.DropChance)
            {
                int itemAmount = Random.Range(dropEntry.MinAmount, dropEntry.MaxAmount + 1);
                Vector3 spawnPos = new Vector3(Random.Range(-1.5f, 1.5f), 0, Random.Range(-1.5f, 1.5f));
                for (int i = 0; i < itemAmount; i++)
                { 
                    Instantiate(dropEntry.Item.Prefab,position - spawnPos, Quaternion.identity);
                }
            }
        }
    }
}
