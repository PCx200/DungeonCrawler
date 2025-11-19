using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

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
        Debug.Log($"Item {item.name} added.");
        return true;
    }
}
