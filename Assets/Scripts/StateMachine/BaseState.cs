using System;
using UnityEngine;

public abstract class BaseState<EState> where EState : Enum
{
    public BaseState(EState key) {
        stateKey = key;
    }

    public EState stateKey { get; private set; }

    public abstract void Enter();
    public abstract void Exit();
    public abstract void Update();

    public abstract EState GetNextState();

    public abstract void OnTriggerEnter(Collider other);
    public abstract void OnTriggerExit(Collider other);
    public abstract void OnCollisionEnter(Collision other);    
}
