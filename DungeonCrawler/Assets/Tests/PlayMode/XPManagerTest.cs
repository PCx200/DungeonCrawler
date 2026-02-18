using NUnit.Framework;
using System.Collections;
using System.Reflection;
using UnityEngine;
using UnityEngine.TestTools;

public class XPManagerTest
{
    [Test]
    public void XP_Increases_OnEnemyDieEvent()
    {
        var playerPrefab = Resources.Load<Player>("TestPrefabs/Player");
        var xpManagerPrefab = Resources.Load<XPManager>("TestPrefabs/XPManager");

        Assert.IsNotNull(xpManagerPrefab, "XPManager prefab missing from Resources/TestPrefabs/XPManager");
        Assert.IsNotNull(playerPrefab, "Player prefab missing from Resources/TestPrefabs/Player");

        var xpManager = Object.Instantiate(xpManagerPrefab);
        var player = Object.Instantiate(playerPrefab);

        typeof(XPManager).GetField("player", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(xpManager, player);

        int startXP = player.ProgressData.XP;

        EventBus.OnEnemyDieEvent.Publish(new EnemyDieEvent { XPGivenAmount = 50 });

        Assert.AreEqual(startXP + 50, player.ProgressData.XP);
    }
}
