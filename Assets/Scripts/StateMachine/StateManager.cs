using System;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    public class StateManager<EState> : MonoBehaviour where EState : Enum
    {
        protected BaseState<EState> currentState;
        protected Dictionary<EState, BaseState<EState>> states = new();

        private bool isInitialized = false;

        void Start() {
            currentState.Enter();
        }

        void Update() {
            EState newState = currentState.GetNextState();

            if (!newState.Equals(currentState.stateKey)) ChangeState(newState);
            if (isInitialized) currentState.Update();
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
            if (!currentState.stateKey.Equals(newState)) {
                currentState.Exit();
                currentState = states[newState];
                currentState.Enter();
            }
            isInitialized = true;
        }
    }
}
