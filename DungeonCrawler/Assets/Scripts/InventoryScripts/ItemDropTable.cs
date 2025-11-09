using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItemDropTable", menuName = "DropableItems/Item Drop Table")]
public class ItemDropTable : ScriptableObject
{
    [Header("Possible Drops")]
    [SerializeField] List<Item> items;

    [Header("Money Drop")]
    [SerializeField] Currency money;

    public List<Item> Items => items;
    public Currency Money => money;
}
