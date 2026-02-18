using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class EnemyDamagedTest
{
    public Enemy enemyPrefab;

    [Test]
    public void Enemy_TakesDamage()
    {
        var enemyPrefab = Resources.Load<Enemy>("TestPrefabs/Goblin"); 
        Assert.IsNotNull(enemyPrefab, "Enemy prefab not found in Resources/TestPrefabs/Goblin");

        var enemy = Object.Instantiate(enemyPrefab);
        float startHP = enemy.CurrentHealth;

        enemy.TakeDamage(new DamageData(10));

        Assert.Less(enemy.CurrentHealth, startHP);
    }
}
