using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] ItemData itemData;
    [SerializeField] float timeToDespawn = 30;

    public ItemData ItemData => itemData;

    private void Start()
    {
        Despawn();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (InventoryManager.Instance.TryAddItem(itemData))
            {
                EventBus.OnItemTaken.Publish(new TakeItemEvent {Item = this});
                Destroy(gameObject);
            }
            else {
                Debug.Log("Inventory full!");
            }
            
        }
    }

    void Despawn()
    {
        Destroy(gameObject, timeToDespawn);
    }
}
