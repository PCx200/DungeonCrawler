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
        
    }

    private void OnDisable()
    {
        EventBus.OnPlayerDamaged.Unsubscribe(OnPlayerDamaged);
    }

    private void OnPlayerDamaged(PlayerDamagedEvent e)
    {
        RefreshUI();
    }
    private void OnLevelUp(LevelUpEvent e)
    {
        RefreshUI();
    }

    public override void RefreshUI()
    {
        hpBar.fillAmount = player.CurrentHealth / player.MaxHealth;
    }
}
