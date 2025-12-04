using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemPresenter : UIPresenter
{
    [SerializeField] Slot slot;
    [SerializeField] Sprite itemSprite;
    [SerializeField] TextMeshProUGUI itemCountText;

    private void OnEnable()
    {
        EventBus.OnItemTaken.Subscribe(OnItemTaken);

    }

    private void OnDisable()
    {
        EventBus.OnItemTaken.Unsubscribe(OnItemTaken);
    }

    public override void RefreshUI()
    {
        gameObject.GetComponent<Image>().sprite = itemSprite;
    }

    void OnItemTaken(TakeItemEvent e)
    {
        if (e.Slot != slot) return;
        itemSprite = e.Item.ItemData.Icon;
        itemCountText.text = $"{e.Slot.Amount}";
        RefreshUI();
    }
}
