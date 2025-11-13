 using UnityEngine;
using UnityEngine.UI;

public class PlayerXPBarPresenter : UIPresenter
{
    [SerializeField] Player player;
    [SerializeField] Image xpBar;

    public override void RefreshUI()
    {
        xpBar.fillAmount = (float)player.ProgressData.XP / player.ProgressData.XPToNextLevel;
    }
    private void OnEnemyDie(EnemyDieEvent e)
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

    private void OnEnable()
    {
        EventBus.OnEnemyDieEvent.Subscribe(OnEnemyDie);
        EventBus.OnLevelUp.Subscribe(OnLevelUp);
        EventBus.OnStatsReset.Subscribe(OnStatsReset);
    }

    private void OnDisable()
    {
        EventBus.OnEnemyDieEvent.Unsubscribe(OnEnemyDie);
        EventBus.OnLevelUp.Unsubscribe(OnLevelUp);
        EventBus.OnStatsReset.Unsubscribe(OnStatsReset);
    }

}
