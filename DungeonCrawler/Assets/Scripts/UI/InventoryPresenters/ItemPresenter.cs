using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemPresenter : UIPresenter
{
    [SerializeField] Slot slot;
    [SerializeField] Sprite itemSprite;
    [SerializeField] TextMeshProUGUI itemCountText;

    private void Awake()
    {
        EventBus.OnItemTaken.Subscribe(OnItemTaken);
        //EventBus.OnPlayerHealed.Subscribe(OnPlayerHealed);
    }

    private void OnDestroy()
    {
        EventBus.OnItemTaken.Unsubscribe(OnItemTaken);
        //EventBus.OnPlayerHealed.Unsubscribe(OnPlayerHealed); 
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
    //void OnPlayerHealed(PlayerHealedEvent e)
    //{
    //    itemCountText.text = $"{slot.Amount}";
    //}
}
