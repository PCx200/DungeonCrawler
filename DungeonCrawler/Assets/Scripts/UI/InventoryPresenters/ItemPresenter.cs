using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemPresenter : UIPresenter
{
    [SerializeField] Slot slot;
    [SerializeField] Image itemImage;
    [SerializeField] TextMeshProUGUI itemCountText;

    private void Awake()
    {
        EventBus.OnItemTaken.Subscribe(OnItemTaken);
    }

    private void OnEnable()
    {
        RefreshUI();
    }

    private void OnDestroy()
    {
        EventBus.OnItemTaken.Unsubscribe(OnItemTaken);
    }

    public override void RefreshUI()
    {
        if (slot == null || slot.ItemData == null) 
        { 
            itemCountText.text = ""; 
            return; 
        }

        itemImage.sprite = slot.ItemData.Icon;

        // dont display the amount if it is one
        if (slot.Amount == 1)
        {
            itemCountText.text = "";
        }
        else
        { 
            itemCountText.text = $"{slot.Amount}";
        }
    }

    void OnItemTaken(TakeItemEvent e)
    {
        if (e.Slot != slot) return;
        RefreshUI();
    }
}
