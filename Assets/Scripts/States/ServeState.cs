using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServeState : BaseState
{
    public override void EnterState()
    {
        base.EnterState();
        ViewController.Instance.ServeView.ShowView();
        ViewController.Instance.ServeView.EvtServePressed.AddListener(OnServeComplete);
    }

    public override void ExitState()
    {
        base.ExitState();
        ViewController.Instance.ServeView.EvtServePressed.RemoveListener(OnServeComplete);
        ViewController.Instance.ServeView.HideView();
    }

    private void OnServeComplete()
    {
        GameStateMachine.Instance.ChangeState(new GoodbyeState());
    }
}
