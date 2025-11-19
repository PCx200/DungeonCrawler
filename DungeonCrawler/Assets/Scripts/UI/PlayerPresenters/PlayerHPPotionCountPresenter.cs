using TMPro;
using UnityEngine;

public class PlayerHPPotionCountPresenter : UIPresenter
{
    [SerializeField] TextMeshProUGUI potionCountText;

    public override void RefreshUI()
    {
        Debug.Log("Refreshed!");
        //potionCountText.text = $"{potionCount}";
    }

    private void OnEnable()
    {
        EventBus.OnItemTaken?.Subscribe(OnPotionTaken);
    }

    private void OnDisable()
    {
        EventBus.OnItemTaken?.Unsubscribe(OnPotionTaken);
    }

    private void OnPotionTaken(TakeItemEvent e)
    {
        if (e.Item.ItemData.name == "HPPotion")
        {
            RefreshUI();
        }
    }
}
