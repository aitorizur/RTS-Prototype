using System;

public class StateMachine<T>
{
    private State<T>[] states;

    public State<T> currentState;
    public T parent;

    public StateMachine(State<T>[] givenStates, T givenParent)
    {
        parent = givenParent;
        states = givenStates;
        foreach (State<T> current in states)
        {
            current.InitializeState(this);
        }
    }

    public void Run()
    {
        currentState.Run();
    }

    public void SwapToState(Type type)
    {
        for (int i = 0; i < states.Length; i++)
        {
            if (states[i].GetType() == type && currentState != states[i])
            {
                if (currentState != null) { currentState.OnExit(); }
                currentState = states[i];
                currentState.OnEnter();
                return;
            }
        }
    }

    public void SwapToStateOrRenter(Type type)
    {
        for (int i = 0; i < states.Length; i++)
        {
            if (states[i].GetType() == type)
            {
                currentState.OnExit();
                currentState = states[i];
                currentState.OnEnter();
                return;
            }
        }
    }
}
