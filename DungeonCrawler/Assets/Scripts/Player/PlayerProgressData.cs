using UnityEngine;

[CreateAssetMenu(fileName = "PlayerProgress", menuName = "Data/Player Progress")]
public class PlayerProgressData : ScriptableObject
{
    [Header("Level & XP")]
    public int Level = 1;
    public int XP = 0;
    public int XPToNextLevel = 100;

    [Header("Current Stats")]
    public float MaxHealth;
    public float MaxAttack;
    public float MaxDefense;
    public float MaxMana;
    public float MaxMovementSpeed;

    public void ResetStats(BaseStatsData baseStats)
    {
        Level = 1;
        XP = 0;
        XPToNextLevel = 100;

        MaxHealth = baseStats.Health;
        MaxAttack = baseStats.Attack;
        MaxDefense = baseStats.Defense;
        MaxMana = baseStats.Mana;
        MaxMovementSpeed = baseStats.MovementSpeed;
    }
}