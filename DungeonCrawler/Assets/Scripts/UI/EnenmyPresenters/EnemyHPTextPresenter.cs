using TMPro;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class EnemyHPTextPresenter : UIPresenter
{
    [SerializeField] Enemy enemy;
    [SerializeField] TextMeshProUGUI hpText;

    public override void RefreshUI()
    {
        hpText.text = $"{enemy.CurrentHealth}/{enemy.MaxHealth}";
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RefreshUI();
    }

    private void OnEnemyDamaged(EnemyDamagedEvent e)
    {
        if (e.Enemy == enemy)
            RefreshUI();
    }

    private void OnEnable()
    {
        EventBus.OnEnemyDamaged.Subscribe(OnEnemyDamaged);
    }
    private void OnDisable()
    {
        EventBus.OnEnemyDamaged.Unsubscribe(OnEnemyDamaged);
    }
}
