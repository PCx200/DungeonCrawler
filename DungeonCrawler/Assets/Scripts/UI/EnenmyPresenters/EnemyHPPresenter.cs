using UnityEngine;
using UnityEngine.UI;

public class EnemyHPPresenter : UIPresenter
{
    [SerializeField] Image hpBar;
    [SerializeField] Enemy enemy;

    public override void RefreshUI()
    {
        hpBar.fillAmount = enemy.CurrentHealth / enemy.MaxHealth;
    }

    private void Start()
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
