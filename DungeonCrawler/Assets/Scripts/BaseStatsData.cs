using UnityEngine;

[CreateAssetMenu(fileName = "NewStats", menuName = "BaseStats/Stats Template")]
public class BaseStatsData : ScriptableObject
{
    [Header("Base Stats")]
    public float Health;
    public float Attack;
    public float Defense;
    public float MovementSpeed;
    public float AttackSpeed;
    public float Mana;
}
