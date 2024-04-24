using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodbyeState : BaseState
{
    public override void EnterState()
    {
        base.EnterState();
        ViewController.Instance.GoodbyeView.ShowView();
        ViewController.Instance.GoodbyeView.EvtNextYokaiPressed.AddListener(OnNextYokai);

        GameStateMachine.Instance.YokaiManager.ActivateYokaiState(YokaiState.Goodbye);
    }

    public override void ExitState()
    {
        base.ExitState();
        ViewController.Instance.GoodbyeView.EvtNextYokaiPressed.RemoveListener(OnNextYokai);
        ViewController.Instance.GoodbyeView.HideView();
    }

    private void OnNextYokai()
    {
        GameStateMachine.Instance.YokaiManager.DestroyCurrentYokai();
        GameStateMachine.Instance.LetterBallHandler.ResetAll();
        GameStateMachine.Instance.CookingManager.ResetAll();
        if (GameStateMachine.Instance.YokaiManager.InstantiateYokai())
        {
            GameStateMachine.Instance.GoToOrderState();
            ViewController.Instance.GoToOrderView();
        }
    }
}
