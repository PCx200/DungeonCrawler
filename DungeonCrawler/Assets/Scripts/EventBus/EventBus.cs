using UnityEngine;

public static class EventBus
{
    public static GameEvent<CurrencyCollectedEvent> OnCurrencyCollected = new GameEvent<CurrencyCollectedEvent>();
    public static GameEvent<EnemyDieEvent> OnEnemyDieEvent = new GameEvent<EnemyDieEvent>();
    public static GameEvent<LevelUpEvent> OnLevelUp = new GameEvent<LevelUpEvent>();
    public static GameEvent<PlayerDeathEvent> OnPlayerDeath = new GameEvent<PlayerDeathEvent>();
    public static GameEvent<PlayerDamagedEvent> OnPlayerDamaged = new GameEvent<PlayerDamagedEvent>();
    public static GameEvent<EnemyDamagedEvent> OnEnemyDamaged = new GameEvent<EnemyDamagedEvent>();
    public static GameEvent<PlayerStatsResetEvent> OnStatsReset = new GameEvent<PlayerStatsResetEvent>();

}
public struct CurrencyCollectedEvent
{
    public int Amount;
}
public struct EnemyDieEvent
{
    public Enemy Enemy;
    public Vector3 Position;
    public int XPGivenAmount;
}
public struct LevelUpEvent
{
    public int NewLevel;
}
public struct PlayerDeathEvent { }
public struct PlayerDamagedEvent 
{
    public float CurrentHealth;
    public float MaxHealth;
}public struct EnemyDamagedEvent 
{
    public Enemy Enemy;
}

public struct PlayerStatsResetEvent { }
