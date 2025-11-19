using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;

    [SerializeField] private int currentAmount;

    public int CurrentAmount => currentAmount;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
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
        currentAmount += e.Amount;
        Debug.Log($"Picked up {e.Amount} coins! Total: {currentAmount}");
        // TODO: Update UI
    }

    public int GetCurrency() => currentAmount;
}
