using UnityEngine;

public static class EventBus
{
    public static GameEvent<CurrencyCollectedEvent> OnCurrencyCollected = new GameEvent<CurrencyCollectedEvent>();
    public static GameEvent<EnemyDieEvent> OnEnemyDieEvent = new GameEvent<EnemyDieEvent>();

}
public struct CurrencyCollectedEvent
{
    public int Amount;
}public struct EnemyDieEvent
{
    public Enemy Enemy;
    public Vector3 Position;
}
