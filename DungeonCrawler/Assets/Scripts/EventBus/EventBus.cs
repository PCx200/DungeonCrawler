using UnityEngine;

public static class EventBus
{
    //econ
    public static GameEvent<CurrencyCollectedEvent> OnCurrencyCollected = new GameEvent<CurrencyCollectedEvent>();
    public static GameEvent<LevelUpEvent> OnLevelUp = new GameEvent<LevelUpEvent>();

    //player
    public static GameEvent<PlayerDamagedEvent> OnPlayerDamaged = new GameEvent<PlayerDamagedEvent>();
    public static GameEvent<PlayerDeathEvent> OnPlayerDeath = new GameEvent<PlayerDeathEvent>();
    public static GameEvent<PlayerStatsResetEvent> OnStatsReset = new GameEvent<PlayerStatsResetEvent>();
    public static GameEvent<PlayerHealedEvent> OnPlayerHealed = new GameEvent<PlayerHealedEvent>();
    
    //enemy
    public static GameEvent<EnemyDieEvent> OnEnemyDieEvent = new GameEvent<EnemyDieEvent>();
    public static GameEvent<EnemyDamagedEvent> OnEnemyDamaged = new GameEvent<EnemyDamagedEvent>();
    
    //item
    public static GameEvent<TakeItemEvent> OnItemTaken = new GameEvent<TakeItemEvent>();
    

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
}
public struct EnemyDamagedEvent 
{
    public Enemy Enemy;
}
public struct PlayerHealedEvent
{
    public float HealAmount;
    public float CurrentHealth;
    public Slot Slot;
}

public struct PlayerStatsResetEvent { }

public struct TakeItemEvent 
{ 
    public Inventory Inventory;
    public Item Item;
    public Slot Slot;
    public int Amount;
}
