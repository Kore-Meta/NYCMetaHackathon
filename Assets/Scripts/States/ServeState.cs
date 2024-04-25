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

        GameStateMachine.Instance.YokaiManager.ActivateYokaiState(YokaiState.WaitToBeServed);
        GameStateMachine.Instance.YokaiManager.AddOnFoodReceivedEvt(ViewController.Instance.ServeView.EvtServePressed);
    }

    public override void ExitState()
    {
        base.ExitState();
        GameStateMachine.Instance.YokaiManager.RemoveOnFoodReceivedEvt(ViewController.Instance.ServeView.EvtServePressed);
        ViewController.Instance.ServeView.EvtServePressed.RemoveListener(OnServeComplete);
        ViewController.Instance.ServeView.HideView();
    }

    private void OnServeComplete()
    {
        GameStateMachine.Instance.AdvanceState();
        ViewController.Instance.AdvanceView();
    }
}
