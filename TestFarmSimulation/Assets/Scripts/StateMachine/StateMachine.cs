using Unity.VisualScripting;
using UnityEngine;

public class StateMachine<T>
{
    private State<T> _currentState;
    private T _owner;

    public StateMachine(T owner)
    {
        _owner = owner;
    }

    public void HandleInput()
    {
        State<T> state = _currentState.HandleInput(_owner);
        if (state != null)
        {
            _currentState = state;
        }
    }

    public void StateUpdate()
    {
        _currentState.Update(_owner);
    }

    private StateMachine() { }
}
