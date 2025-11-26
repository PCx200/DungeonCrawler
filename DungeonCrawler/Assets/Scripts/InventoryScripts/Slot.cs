using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    enum SlotType {Empty, Helmet, Chestplate, Leggings, Gloves, Boots, Weapon, HPPotion};
    [SerializeField] SlotType slotType;
    [SerializeField] Sprite background;     
    [SerializeField] bool isEmpty;
    [SerializeField] int amount;
    public ItemData ItemData;
    public TextMeshProUGUI CountText;

    public bool IsEmpty => isEmpty;
    public int Amount => amount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //gameObject.GetComponent<Image>().sprite = itemSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseAmount()
    {
        amount++;
    }
}
