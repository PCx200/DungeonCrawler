using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class BreakableTest
{
    [UnityTest]
    public System.Collections.IEnumerator Breakable_Is_Destroyed_When_Taking_Damage()
    {
        var obj = new GameObject("Breakable");
        var breakable = obj.AddComponent<Breakable>();

        breakable.GetType().GetField("stats",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(breakable, ScriptableObject.CreateInstance<BaseStatsData>());

        breakable.GetType().GetField("dropTable",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(breakable, ScriptableObject.CreateInstance<ItemDropTable>());

        breakable.TakeDamage(new DamageData(10));

        yield return null;

        Assert.IsTrue(breakable == null || breakable.Equals(null),
            "Breakable should be destroyed after TakeDamage is called.");
    }
}
