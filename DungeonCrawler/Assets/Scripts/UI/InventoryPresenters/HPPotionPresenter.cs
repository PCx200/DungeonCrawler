using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HPPotionPresenter : UIPresenter
{
    [SerializeField] Slot slot;
    [SerializeField] TextMeshProUGUI itemCountText;

    private void OnEnable()
    {
        EventBus.OnPlayerHealed.Subscribe(OnPlayerHealed);
    }

    private void OnDisable()
    {
        EventBus.OnPlayerHealed.Unsubscribe(OnPlayerHealed);
    }

    public override void RefreshUI()
    {
        itemCountText.text = $"{slot.Amount}";
    }

    void OnPlayerHealed(PlayerHealedEvent e)
    {
        RefreshUI();
    }
}
