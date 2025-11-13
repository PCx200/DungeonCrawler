using TMPro;
using UnityEngine;

public class PlayerLevelPresenter : UIPresenter
{
    [SerializeField] TextMeshProUGUI playerLevelText;
    [SerializeField] Player player;

    public override void RefreshUI()
    {
        playerLevelText.text = $"{player.CurrentLevel}";
    }

    void Start()
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
        EventBus.OnLevelUp.Subscribe(OnLevelUp);
        EventBus.OnStatsReset.Subscribe(OnStatsReset);
    }

    private void OnDisable()
    {
        EventBus.OnLevelUp.Unsubscribe(OnLevelUp);
        EventBus.OnStatsReset.Unsubscribe(OnStatsReset);
    }
}
