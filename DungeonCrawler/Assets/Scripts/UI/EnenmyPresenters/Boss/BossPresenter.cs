using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossPresenter : UIPresenter
{
    [SerializeField] Enemy boss;
   
    [SerializeField] Image hpBar;

    [SerializeField] TextMeshProUGUI bossName;
    public override void RefreshUI()
    {
        hpBar.fillAmount = boss.CurrentHealth / boss.MaxHealth;
    }

    private void Start()
    {
        bossName.text = boss.GetName();
        RefreshUI();
    }

    private void OnEnemyDamaged(EnemyDamagedEvent e)
    {
        if (e.Enemy == boss)
            RefreshUI();
    }
    private void OnEnemyDie(EnemyDieEvent e)
    {
        if (e.Enemy == boss)
            Destroy(gameObject);
    }

    private void OnEnable()
    {
        EventBus.OnEnemyDamaged.Subscribe(OnEnemyDamaged);
        EventBus.OnEnemyDieEvent.Subscribe(OnEnemyDie);
    }
    private void OnDisable()
    {
        EventBus.OnEnemyDamaged.Unsubscribe(OnEnemyDamaged);
        EventBus.OnEnemyDieEvent.Unsubscribe(OnEnemyDie);
    }
}
