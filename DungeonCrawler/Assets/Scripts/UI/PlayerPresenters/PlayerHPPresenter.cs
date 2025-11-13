using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class PlayerHPPresenter : UIPresenter
{

    [SerializeField] Image hpBar;
    [SerializeField] Player player;


    private void OnEnable()
    {
        EventBus.OnPlayerDamaged.Subscribe(OnPlayerDamaged);
        EventBus.OnLevelUp.Subscribe(OnLevelUp);
        EventBus.OnStatsReset.Subscribe(OnStatsReset);

    }

    private void OnDisable()
    {
        EventBus.OnPlayerDamaged.Unsubscribe(OnPlayerDamaged);
        EventBus.OnLevelUp.Unsubscribe(OnLevelUp);
        EventBus.OnStatsReset.Unsubscribe(OnStatsReset);
    }

    private void OnPlayerDamaged(PlayerDamagedEvent e)
    {
        RefreshUI();
    }
    private void OnLevelUp(LevelUpEvent e)
    {
        RefreshUI();
    }
    private void OnStatsReset(PlayerStatsResetEvent e)
    {
        RefreshUI();
    }

    public override void RefreshUI()
    {
        hpBar.fillAmount = player.CurrentHealth / player.MaxHealth;
    }
}
