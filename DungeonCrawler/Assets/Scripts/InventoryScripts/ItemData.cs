using UnityEngine;
using UnityEngine.UIElements.Experimental;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    [Header("General Info")]
    [SerializeField] string itemName;
    [SerializeField] Sprite icon;

    [Header("Stacking")]
    [SerializeField] bool isStackable;
    [SerializeField] int maxAmount;

    [Header("World Object")]
    [SerializeField] GameObject prefab;

    public bool IsStackable => isStackable;
    public int MaxAmount => maxAmount;
    public GameObject Prefab => prefab;
}
