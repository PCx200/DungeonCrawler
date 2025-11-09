using UnityEngine;

[System.Serializable]
public class Item
{
    [SerializeField] GameObject prefab; // item that is going to be dropped
    [SerializeField, Range(0f, 1f)] float dropChance; // the chance that item is dropped
    [SerializeField] int minDrop;
    [SerializeField] int maxDrop;

    public GameObject Prefab => prefab;
    public float DropChance => dropChance;
    public int MinDrop => minDrop;
    public int MaxDrop => maxDrop;
}
