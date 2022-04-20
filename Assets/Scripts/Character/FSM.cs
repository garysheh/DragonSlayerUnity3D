using System;
using System.Collections.Generic;
using UnityEngine;

public class FSM
{
    private EnemyStates currentState;

    //  Give a state and return all possible states, where it can go to, with a consition 
    private Dictionary<Type, List<Transition>> transitions = new Dictionary<Type, List<Transition>>();
    //  All possible states, where current state can go to, with a consition
    private List<Transition> currentTransitions = new List<Transition>();
    //  All possible states, where any state can go to, with a consition
    private List<Transition> anyTransitions = new List<Transition>();
    //  A self loop dead state,  
    private static List<Transition> EmptyTransitions = new List<Transition>(0);


    public void Tick()
    {
        var transition = GetTransition();
        if (transition != null)
        {
            SetState(transition.To);
        }
        //Debug.Log(currentState);
        currentState?.Tick();
    }

    public void SetState(EnemyStates state)
    {
        //  if self loop, no futher action needed and return to self
        if (state == currentState)
        {
            return;
        }
        //  Exiting current state
        currentState?.OnExit();
        currentState = state;

        transitions.TryGetValue(currentState.GetType(), out currentTransitions);
        if (currentTransitions == null)
        {
            currentTransitions = EmptyTransitions;
        }

        //  Entering new state
        currentState.OnEnter();
    }

    //  if condition is valid, go from state "from" to state "to'
    public void AddTransition(Func<bool> condition, EnemyStates from, EnemyStates to)
    {
        if (this.transitions.TryGetValue(from.GetType(), out var transitions) == false)
        {
            transitions = new List<Transition>();
            this.transitions.Add(from.GetType(), transitions);
        }
        transitions.Add(new Transition(condition, to));
    }

    public void AddAnyTransition(Func<bool> condition, EnemyStates to)
    {
        anyTransitions.Add(new Transition(condition, to));
    }

    private class Transition
    {
        //  Condition to enter the state
        public Func<bool> Condition { get; }
        //  The state wants to go
        public EnemyStates To { get; }

        public Transition (Func<bool> condition, EnemyStates to)
        {
            Condition = condition;
            To = to;
        }
    }

    private Transition GetTransition()
    {
        foreach (var transition in anyTransitions)
        {
            if (transition.Condition())
            {
                return transition;
            }
        }
        foreach (var transition in currentTransitions)
        { 
            //Debug.Log(transition.Condition);
            if (transition.Condition())
            {
                return transition;
            }
        }
        return null;
    }
}
