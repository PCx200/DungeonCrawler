using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GameEventTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void Publish_InvokesSubscriber() 
    { 
        var testEvent = new GameEvent<int>(); 
        int received = 0;
        testEvent.Subscribe(v => received = v);
        testEvent.Publish(42);
        Assert.AreEqual(42, received); 
    }

    [Test] public void Publish_DoesNotInvoke_AfterUnsubscribe() 
    {
        var testEvent = new GameEvent<int>();
        int received = 0;
        void Handler(int v) => received = v;
        testEvent.Subscribe(Handler);
        testEvent.Unsubscribe(Handler);
        testEvent.Publish(10);
        Assert.AreEqual(0, received, "Handler should not be invoked after unsubscribing."); 
    }
}
