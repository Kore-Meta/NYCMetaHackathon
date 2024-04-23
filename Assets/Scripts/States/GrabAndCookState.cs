using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabAndCookState : BaseState
{
    public override void EnterState()
    {
        base.EnterState();
        ViewController.Instance.GrabAndCookView.ShowView();
        ViewController.Instance.GrabAndCookView.EvtCookCompletePressed.AddListener(OnCookComplete);
    }

    public override void ExitState()
    {
        base.ExitState();
        ViewController.Instance.GrabAndCookView.EvtCookCompletePressed.RemoveListener(OnCookComplete);
        ViewController.Instance.GrabAndCookView.HideView();
    }

    private void OnCookComplete()
    {
        if (GameStateMachine.Instance.CookingManager.isCookingDone)
        {
            GameStateMachine.Instance.ChangeState(new ServeState());
        }
    }
}
