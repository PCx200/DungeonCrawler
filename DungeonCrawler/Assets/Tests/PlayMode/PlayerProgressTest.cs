using NUnit.Framework;
using UnityEngine;

public class PlayerProgressTest
{
    [Test]
    public void ResetStats_ResetsValues()
    {
        var baseStats = ScriptableObject.CreateInstance<BaseStatsData>();
        baseStats.Health = 100;
        baseStats.Attack = 10;
        baseStats.Defense = 5;
        baseStats.Mana = 20;
        baseStats.MovementSpeed = 3;

        var progress = ScriptableObject.CreateInstance<PlayerProgressData>();
        progress.Level = 5;
        progress.XP = 200;
        progress.MaxHealth = 999;

        progress.ResetStats(baseStats);

        Assert.AreEqual(1, progress.Level);
        Assert.AreEqual(0, progress.XP);
        Assert.AreEqual(100, progress.MaxHealth);
    }
}
