using UnityEngine;

public class CurrencyPicker : MonoBehaviour
{
    int amount;

    public void Initialize(int value)
    { 
        amount = value;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EventBus.OnCurrencyCollected.Publish(new CurrencyCollectedEvent { Amount = amount });
            Destroy(gameObject);
        }
    }
}
