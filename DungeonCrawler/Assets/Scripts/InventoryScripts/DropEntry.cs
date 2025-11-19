using UnityEngine;

[System.Serializable]
public class DropEntry
{
    [SerializeField] ItemData item; // item that is going to be dropped
    [SerializeField, Range(0f, 1f)] float dropChance; // the chance that item is dropped
    [SerializeField] int minAmount;
    [SerializeField] int maxAmount;

    public ItemData Item => item;
    public float DropChance => dropChance;
    public int MinAmount => minAmount;
    public int MaxAmount => maxAmount;
}
