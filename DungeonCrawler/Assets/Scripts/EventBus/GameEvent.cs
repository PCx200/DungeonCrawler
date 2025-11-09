using System;
using UnityEngine;

public class GameEvent<T>
{
    event Action<T> subscribers;

    public void Subscribe(Action<T> subscriber)
    { 
        subscribers += subscriber;
    }
    public void Unsubscribe(Action<T> subscriber)
    { 
        subscribers -= subscriber;
    }

    public void Publish(T data)
    { 
        subscribers?.Invoke(data);  
    }
}
