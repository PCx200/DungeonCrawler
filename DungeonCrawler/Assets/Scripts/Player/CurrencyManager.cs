using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    [SerializeField] private int totalCurrency;

    private void OnEnable()
    {
        EventBus.OnCurrencyCollected.Subscribe(OnCurrencyCollected);
    }

    private void OnDisable()
    {
        EventBus.OnCurrencyCollected.Unsubscribe(OnCurrencyCollected);
    }

    private void OnCurrencyCollected(CurrencyCollectedEvent e)
    {
        totalCurrency += e.Amount;
        Debug.Log($"Picked up {e.Amount} coins! Total: {totalCurrency}");
        // TODO: Update UI
    }

    public int GetCurrency() => totalCurrency;
}
