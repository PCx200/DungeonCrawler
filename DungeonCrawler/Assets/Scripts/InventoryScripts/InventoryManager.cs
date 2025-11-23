using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [SerializeField] List<ItemData> items = new List<ItemData>();
    Dictionary<Slot, ItemData> slots = new Dictionary<Slot, ItemData>();// ITem not ItemData
    [SerializeField] List<Slot> slotsList = new List<Slot>();

    // list slot -> 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else 
        { 
            Destroy(gameObject);
        }
    }
    public bool TryAddItem(ItemData item)
    {
        //Debug.Log(item.IsStackable);

        //if (item.IsStackable)
        //{
        //    int count = items.FindAll(i => i == item).Count;

        //    if (count < item.MaxAmount)
        //    {
        //        items.Add(item);
        //    }
        //    else
        //    {
        //        Debug.Log("Full capacity");
        //        return false;
        //    }
        //}
        //else 
        //{ 
        //    items.Add(item);
        //}

        // loop thr the keys from the dic -> check the values 
        foreach(var keyValuePair in slots)
        {
            if (keyValuePair.Value == item && keyValuePair.Key.Amount < item.MaxAmount)
            {
                keyValuePair.Key.IncreaseAmount();
                Debug.Log(keyValuePair.Key.Amount);
            }
        }
        
        for (int i = 0; i < slotsList.Count; i++)
        {
            if (slotsList[i].IsEmpty)
            {
                if (item.IsStackable)
                { 
                    
                }
                else
                {
                    slots.Add(slotsList[i], item);
                    break;
                }
                
            }
            else 
            {
                i++;
            }
        }
        //Debug.Log($"Item {item.name} added. Count: {items.FindAll(i => i == item).Count}");
        return true;
    }
}
