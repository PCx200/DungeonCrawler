using UnityEngine;

public class PlayerDeathScreenPresenter : UIPresenter
{
    [SerializeField] GameObject deathScreen;

    private void OnEnable()
    {
        EventBus.OnPlayerDeath.Subscribe(PlayerDeath);
    }

    private void OnDisable()
    {
        EventBus.OnPlayerDeath.Unsubscribe(PlayerDeath);
    }

    public override void RefreshUI()
    {
        deathScreen.SetActive(true);
    }

    void PlayerDeath(PlayerDeathEvent e)
    { 
        RefreshUI();
    }

    
}
