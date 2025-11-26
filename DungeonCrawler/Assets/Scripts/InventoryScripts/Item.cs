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
                Inventory inventory = other.GetComponentInChildren<Inventory>();

                Slot slot = inventory.TryAddItem(this);

                if (slot != null)
                {
                    EventBus.OnItemTaken.Publish(new TakeItemEvent
                    {
                        Inventory = inventory,
                        Item = this,
                        Slot = slot,
                        Amount = slot.Amount
                    });

                    Destroy(gameObject);
                }
                else
                {
                    Debug.Log("Inventory full!");
                }
            }
        }

        void Despawn()
        {
            Destroy(gameObject, timeToDespawn);
        }
    }
