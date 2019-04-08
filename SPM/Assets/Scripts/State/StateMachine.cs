using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class StateMachine : MonoBehaviour
{

    [SerializeField] private State[] states;
    private State currentState;
    private Dictionary<Type, State> stateDictionary = new Dictionary<Type, State>();

    protected virtual void Awake()
    {
        foreach (State state in states)
        {
            State instance = Instantiate(state);
            instance.Initialize(this);
            stateDictionary.Add(instance.GetType(), instance);
            if (currentState == null)
            {
                currentState = instance;
            }
        }
        currentState.Enter();
    }

    public void Transition<T>() where T : State
    {
        currentState.Exit();
        currentState = stateDictionary[typeof(T)];
        currentState.Enter();
    }

    // Update is called once per frame
    private void Update()
    {
        currentState.HandleUpdate();
    }
}