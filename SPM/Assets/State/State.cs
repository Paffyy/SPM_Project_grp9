using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : ScriptableObject
{

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void HandleUpdate() { }
    public virtual void HandleLateUpdate() { }
    public virtual void Initialize(StateMachine player) { }
}
