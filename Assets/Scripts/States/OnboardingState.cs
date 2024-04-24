using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnboardingState : BaseState
{
    public override void EnterState()
    {
        base.EnterState();
        GameStateMachine.Instance.YokaiManager.InstantiateYokai();
        ViewController.Instance.OnboardingView.ShowView();
        ViewController.Instance.OnboardingView.EvtStartGamePressed.AddListener(OnStartGame);

        float waitTime = GameStateMachine.Instance.YokaiManager.ActivateYokaiState(YokaiState.Onboarding);
        ViewController.Instance.Invoke("AdvanceView", waitTime);
    }

    public override void ExitState()
    {
        base.ExitState();
        ViewController.Instance.OnboardingView.EvtStartGamePressed.RemoveListener(OnStartGame);
        ViewController.Instance.OnboardingView.HideView();
    }

    private void OnStartGame()
    {
        GameStateMachine.Instance.AdvanceState();
    }
}
