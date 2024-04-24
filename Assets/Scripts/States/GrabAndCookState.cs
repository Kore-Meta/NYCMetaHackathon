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

        GameStateMachine.Instance.CookingManager.EvtCookingDone.AddListener(OnCookComplete);
    }

    public override void ExitState()
    {
        base.ExitState();

        GameStateMachine.Instance.CookingManager.EvtCookingDone.RemoveListener(OnCookComplete);

        ViewController.Instance.GrabAndCookView.EvtCookCompletePressed.RemoveListener(OnCookComplete);
        ViewController.Instance.GrabAndCookView.HideView();
    }

    private void OnCookComplete()
    {
        if (GameStateMachine.Instance.CookingManager.isCookingDone)
        {
            GameStateMachine.Instance.AdvanceState();
            ViewController.Instance.AdvanceView();
        }
    }
}
