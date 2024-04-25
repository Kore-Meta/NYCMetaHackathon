using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{
    public virtual void EnterState() { }
    public virtual void UpdateState() { }
    public virtual void ExitState() { }
}
