using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderState : BaseState
{
    public override void EnterState()
    {
        base.EnterState();
        ViewController.Instance.OrderView.ShowView();
        ViewController.Instance.OrderView.EvtPlaceOrderPressed.AddListener(OnPlaceOrderComplete);

        float waitTime = GameStateMachine.Instance.YokaiManager.ActivateYokaiState(YokaiState.Order);
        ViewController.Instance.Invoke("AdvanceView", waitTime);
    }

    public override void ExitState()
    {
        base.ExitState();

        ViewController.Instance.OrderView.EvtPlaceOrderPressed.RemoveListener(OnPlaceOrderComplete);
        ViewController.Instance.OrderView.HideView();
    }

    private void OnPlaceOrderComplete()
    {
        GameStateMachine.Instance.LetterBallHandler.questionSO = GameStateMachine.Instance.YokaiManager.GetCurrentQuestion();
        GameStateMachine.Instance.LetterBallHandler.SetUpBalls();
        GameStateMachine.Instance.CookingManager.questionSO = GameStateMachine.Instance.YokaiManager.GetCurrentQuestion();
        GameStateMachine.Instance.CookingManager.SetOrderText();
        
        GameStateMachine.Instance.AdvanceState();
    }
}
