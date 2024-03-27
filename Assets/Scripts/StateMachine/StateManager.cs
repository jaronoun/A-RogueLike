using System;
using System.Collections.Generic;
using UnityEngine;

public class StateManager<EState> : MonoBehaviour where EState : Enum
{
    protected BaseState<EState> currentState;
    protected Dictionary<EState, BaseState<EState>> states = new();

    protected bool isInitialized = false;

    void Start() {
        currentState.EnterState();
    }

    void Update() {
        EState newState = currentState.GetNextState();

        if (!newState.Equals(currentState.stateKey)) ChangeState(newState);
        if (isInitialized) currentState.UpdateState();
    }

    void OnTriggerEnter(Collider other) {
        currentState.OnTriggerEnter(other);
    }

    void OnTriggerExit(Collider other) {
        currentState.OnTriggerExit(other);
    }

    void OnCollisionEnter(Collision other) {
        currentState.OnCollisionEnter(other);
    }

    public void ChangeState(EState newState) {
        isInitialized = false;
        currentState.ExitState();
        currentState = states[newState];
        currentState.EnterState();
        isInitialized = true;
    }

}
