using TMPro;
using UnityEngine;

public class PlayerMoneyPresenter : UIPresenter
{
    [SerializeField] TextMeshProUGUI moneyText;

    private void OnEnable()
    {
        EventBus.OnCurrencyCollected.Subscribe(OnMoneyCollected);
        RefreshUI();
    }

    private void OnDisable()
    {
        EventBus.OnCurrencyCollected.Unsubscribe(OnMoneyCollected);
    }

    public override void RefreshUI()
    {
         moneyText.text = $"{CurrencyManager.Instance.CurrentAmount}";
    }
    void OnMoneyCollected(CurrencyCollectedEvent e)
    { 
        RefreshUI();
    }

}
