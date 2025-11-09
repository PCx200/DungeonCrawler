using UnityEditor.Experimental.GraphView;
using UnityEditor.Search;
using UnityEngine;

public class DropSystem : MonoBehaviour
{
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

            CurrencyPicker picker = moneyInstance.GetComponent<CurrencyPicker>();
            if (picker != null)
                picker.Initialize(amount);
        }

        // Drop items if lucky 
        foreach (var item in dropTable.Items)
        {
            if (Random.value <= item.DropChance)
            {
                int itemAmount = Random.Range(item.MinDrop, item.MaxDrop + 1);
                Vector3 spawnPos = new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f));
                for (int i = 0; i < itemAmount; i++)
                    Instantiate(item.Prefab,position - spawnPos, Quaternion.identity);
            }
        }
    }
}
