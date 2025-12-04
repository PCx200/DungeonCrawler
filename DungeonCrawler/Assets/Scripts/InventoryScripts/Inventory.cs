using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] List<ItemData> items = new List<ItemData>();
    [SerializeField] List<Slot> slots = new List<Slot>();

    [SerializeField] Transform inventorySlots;


    private void Start()
    {
        for (int i = 0; i < inventorySlots.childCount; i++) 
        {
            slots.Add(inventorySlots.GetChild(i).GetComponent<Slot>());
        }
    }

    public Slot TryAddItem(Item item)
    {
        if (item.ItemData.name == "HPPotion")
        {
            Slot slot = slots[0];
            if (slot.Amount < item.ItemData.MaxAmount)
            {
                slot.IncreaseAmount();
                slot.ItemData = item.ItemData;
                return slot;
            }
            return null;
        }
        for (int i = 1; i < slots.Count; i++)
        {
            Slot slot = slots[i];

            if (slot.ItemData == null)
            {
                slot.ItemData = item.ItemData;
                slot.IncreaseAmount();
                return slot;
            }

            if (slot.ItemData == item.ItemData && slot.Amount < item.ItemData.MaxAmount)
            {
                slot.IncreaseAmount();
                return slot;
            }
        }

        return null;
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item.ItemData);
    }

    public Slot GetSlot(int index)
    {
        return slots[index];
    }
}
