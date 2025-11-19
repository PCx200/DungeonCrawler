using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItemDropTable", menuName = "DropableItems/Item Drop Table")]
public class ItemDropTable : ScriptableObject
{
    [Header("Possible Drops")]
    [SerializeField] List<DropEntry> items;

    [Header("Money Drop")]
    [SerializeField] Currency money;

    public List<DropEntry> Items => items;
    public Currency Money => money;
}
    