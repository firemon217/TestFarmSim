using UnityEngine;

public abstract class State<T>
{
    public virtual void Enter(T owner) { }
    public virtual void Exit(T owner) { }
    public virtual State<T> HandleInput(T owner) { return this; }
    public virtual void Update(T owner) { }
}
